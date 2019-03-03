using NetCraft.Config;
using NetCraft.Core.Network;
using NetCraft.Core.Packets;
using NetCraft.Logging;
using NetCraft.Plugin;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;

namespace NetCraft.Network
{
    public class Server
    {
        public PacketManager PacketManager { get; private set; }
        public PluginManager PluginManager { get; private set; }
        public LoggerManager LoggerManager { get; private set; }

        public Configuration Configuration { get; private set; }

        private TcpListener _listener;
        private bool _running;
        private Thread _acceptingClientsThread;

        private List<Client> _clients;
        private List<Client> _clientsToRemove;

        public event EventHandler OnTick;
        public event DisconnectClientHandler OnDisconnect;

        private Logger _logger;

        private const string SERVER_CONFIG_FILE = "server-config.json";

        public Server(PacketManager packetManager, PluginManager pluginManager, LoggerManager loggerManager)
        {
            PacketManager = packetManager;
            PluginManager = pluginManager;
            LoggerManager = loggerManager;
            _logger = new Logger(LoggerManager, "Server");
            _clients = new List<Client>();
            _clientsToRemove = new List<Client>();
            Configuration = new Configuration();
            LoadConfiguration();
        }

        public void LoadConfiguration()
        {
            try
            {
                Configuration = JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(SERVER_CONFIG_FILE),
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                        Formatting = Formatting.Indented,
                    });
            }
            catch (FileNotFoundException)
            {
                _logger.Warning($"Server configuration file {SERVER_CONFIG_FILE} not found.");
            }
            catch (Exception exception)
            {
                _logger.Error($"Unable to read configuration file {SERVER_CONFIG_FILE} :");
                _logger.Error($"{exception.GetType()} : {exception.Message}");
                _logger.Error(exception.StackTrace);
            }
        }

        public void SaveConfiguration()
        {
            try
            {
                File.WriteAllText(SERVER_CONFIG_FILE, JsonConvert.SerializeObject(Configuration,
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                        Formatting = Formatting.Indented,
                    }));
            }
            catch (Exception exception)
            {
                _logger.Error($"Unable to write configuration file {SERVER_CONFIG_FILE} :");
                _logger.Error($"{exception.GetType()} : {exception.Message}");
                _logger.Error(exception.StackTrace);
            }
        }

        public Logger GetLogger(IPlugin plugin) => LoggerManager.GetLogger(plugin);

        public void Stop()
        {
            UnloadPlugins();
            _running = false;
            _acceptingClientsThread.Join();
            foreach (var client in _clients)
            {
                client.Dispose();
            }
            _clients.Clear();
            _listener.Stop();
        }

        public void UnloadPlugins()
        {
            PluginManager.UnloadPlugins(this);
            PacketManager.UnregisterAllPacketHandlers();
            PacketManager.UnregisterAllPacketIds();
        }

        public void Start(IPAddress ip, int port)
        {
            if (_listener != null || _running)
            {
                throw new Exception("Server is already running!");
            }
            _running = true;
            _listener = new TcpListener(ip, port);
            _listener.Start();
            _acceptingClientsThread = new Thread(new ThreadStart(() =>
            {
                while (_running)
                {
                    Thread.Sleep(1);
                    if (_listener.Pending())
                    {
                        var client = new Client(_listener.AcceptTcpClient());
                        _clients.Add(client);
                    }
                }
            }));
            _acceptingClientsThread.Start();
            while (_running)
            {
                DoTick();
                Thread.Sleep(50);
            }
        }

        private void DoTick()
        {
            foreach (var client in _clients)
            {
                try
                {
                    foreach (var packet in client.PendingPackets)
                    {
                        PacketManager.SendPacket(client.JavaDataStream, packet);
                    }
                    client.PendingPackets.Clear();
                    PacketManager.HandlePackets(client);
                }
                catch (Exception exception)
                {
                    _logger.Error($"Exception of type {exception.GetType()} occurred: {exception.Message}");
                    _logger.Error(exception.StackTrace);
                    OnDisconnect?.Invoke(this, client);
                    _clientsToRemove.Add(client);
                }
            }
            foreach(var clientToRemove in _clientsToRemove)
            {
                _clients.Remove(clientToRemove);
                clientToRemove.Dispose();
            }
            _clientsToRemove.Clear();
            OnTick?.Invoke(this, new EventArgs());
        }

        public void LoadPlugins()
        {
            var pluginsPath = Configuration.PluginDirectory ?? "./Plugins";
            var pluginList = Configuration.Plugins ?? new string[] { };
            foreach (var pluginPath in pluginList)
            {
                if (!pluginPath.ToLower().EndsWith(".dll"))
                    continue;
                var assembly = Assembly.LoadFrom(Path.Combine(pluginsPath, pluginPath));
                Type pluginType = null;
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (type.GetInterfaces().Contains(typeof(IPlugin)))
                    {
                        pluginType = type;
                        break;
                    }
                }
                if (pluginType == null)
                    continue;
                var plugin = (IPlugin)Activator.CreateInstance(pluginType);
                PluginManager.LoadPlugin(this, plugin);
            }

            var registeredPacketsCount = PacketManager.GetRegisteredPacketsCount();
            var handledPacketsCount = PacketManager.GetHandledPacketsCount();
            var pluginsCount = PluginManager.Plugins.Count;
            
            _logger.Info($"{registeredPacketsCount} registered packet{(registeredPacketsCount > 1 ? "s" : string.Empty)}");
            _logger.Info($"{handledPacketsCount} handled packet{(handledPacketsCount > 1 ? "s" : string.Empty)}");
            _logger.Info($"{pluginsCount} loaded plugin{(pluginsCount > 1 ? "s" : string.Empty)}");
        }
    }
}

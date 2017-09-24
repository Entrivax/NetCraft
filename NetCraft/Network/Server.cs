using NetCraft.Core.Network;
using NetCraft.Core.Packets;
using NetCraft.Plugin;
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

        private TcpListener _listener;
        private bool _running;
        private Thread _acceptingClientsThread;

        private List<Client> _clients;
        private List<Client> _clientsToRemove;

        public event System.EventHandler OnTick;
        public event DisconnectClientHandler OnDisconnect;

        public Server(PacketManager packetManager, PluginManager pluginManager)
        {
            PacketManager = packetManager;
            PluginManager = pluginManager;
            _clients = new List<Client>();
            _clientsToRemove = new List<Client>();
        }

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
                    Console.WriteLine($"Exception of type {exception.GetType()} occurred: {exception.Message}");
                    Console.WriteLine(exception.StackTrace);
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
            var pluginPaths = Directory.EnumerateFiles("./Plugins");
            foreach (var pluginPath in pluginPaths)
            {
                if (!pluginPath.ToLower().EndsWith(".dll"))
                    continue;
                var assembly = Assembly.LoadFrom(pluginPath);
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

            Console.WriteLine($"{registeredPacketsCount} registered packet{(registeredPacketsCount > 0 ? "s" : string.Empty)}");
            Console.WriteLine($"{handledPacketsCount} handled packet{(handledPacketsCount > 0 ? "s" : string.Empty)}");
            Console.WriteLine($"{pluginsCount} loaded plugin{(pluginsCount > 0 ? "s" : string.Empty)}");
        }
    }
}

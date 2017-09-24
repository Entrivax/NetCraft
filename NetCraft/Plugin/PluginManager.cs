using NetCraft.Network;
using System;
using System.Collections.Generic;

namespace NetCraft.Plugin
{
    public class PluginManager
    {
        public List<IPlugin> Plugins { get; private set; }

        private Dictionary<Type, List<EventHandlerParams>> _handlers;

        public PluginManager()
        {
            Plugins = new List<IPlugin>();
            _handlers = new Dictionary<Type, List<EventHandlerParams>>();
        }

        public void LoadPlugin(Server server, IPlugin plugin)
        {
            plugin.Load(server);
            Plugins.Add(plugin);
        }

        public void UnloadPlugin(Server server, IPlugin plugin)
        {
            plugin.Unload(server);
            Plugins.Remove(plugin);
        }

        public void UnloadPlugins(Server server)
        {
            var plugins = Plugins.ToArray();
            foreach (var plugin in plugins)
            {
                UnloadPlugin(server, plugin);
            }
        }

        public void BroadcastEvent<T>(T ev)
        {
            var type = typeof(T);
            if (_handlers.ContainsKey(type))
            {
                foreach (var eventHandlerParams in _handlers[type])
                {
                    eventHandlerParams.MethodInfo.Invoke(eventHandlerParams.Instance, new object[] { ev });
                }
            }
        }

        public void RegisterEventHandler(object listener)
        {
            var type = listener.GetType();

            foreach (var method in type.GetMethods())
            {
                var attributes = method.GetCustomAttributes(typeof(EventHandler), false);
                if (attributes.Length > 1)
                    throw new Exception("Cannot use EventHandler attribute more than one time on a method!");
                if (attributes.Length < 1)
                    continue;
                var eventType = ((EventHandler)attributes[0]).EventType;
                var eventHandlerParams = new EventHandlerParams
                {
                    MethodInfo = method,
                    Instance = listener
                };
                if (_handlers.ContainsKey(eventType))
                    _handlers[eventType].Add(eventHandlerParams);
                else
                    _handlers.Add(eventType, new List<EventHandlerParams> { eventHandlerParams });
            }
        }
    }
}

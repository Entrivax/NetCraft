using NetCraft.Network;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NetCraft.Plugin
{
    public class PluginManager
    {
        public List<IPlugin> Plugins { get; private set; }

        private Dictionary<Type, List<EventListenerParams>> _handlers;

        public PluginManager()
        {
            Plugins = new List<IPlugin>();
            _handlers = new Dictionary<Type, List<EventListenerParams>>();
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
                    eventHandlerParams.Delegate(eventHandlerParams.Instance, ev);
                }
            }
        }

        public void RegisterEventHandler<T>(T listener)
        {
            var type = typeof(T);

            foreach (var method in type.GetMethods())
            {
                var attributes = method.GetCustomAttributes(typeof(EventListener), false);
                if (attributes.Length > 1)
                    throw new Exception("Cannot use EventHandler attribute more than one time on a method!");
                if (attributes.Length < 1)
                    continue;
                var eventType = ((EventListener)attributes[0]).EventType;

                var paramType1 = Expression.Parameter(type);
                var paramType2 = Expression.Parameter(typeof(object));
                var convert = Expression.Convert(paramType2, eventType);
                var methodBody = Expression.Call(paramType1, method, convert);

                var del = Expression.Lambda<Action<T, object>>(methodBody, paramType1, paramType2).Compile();
                var eventHandlerParams = new EventListenerParams
                {
                    Instance = listener,
                    Delegate = (object instance, object ev) => del((T)instance, ev)
                };
                if (_handlers.ContainsKey(eventType))
                    _handlers[eventType].Add(eventHandlerParams);
                else
                    _handlers.Add(eventType, new List<EventListenerParams> { eventHandlerParams });
            }
        }
    }
}

using NetCraft.Network;
using System.Collections.Generic;

namespace NetCraft.Plugin
{
    public class PluginManager
    {
        public List<IPlugin> Plugins { get; private set; }

        public PluginManager()
        {
            Plugins = new List<IPlugin>();
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
    }
}

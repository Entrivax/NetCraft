using NetCraft.Network;

namespace NetCraft.Plugin
{
    public interface IPlugin
    {
        string Author { get; }
        string Description { get; }
        string Name { get; }
        string Version { get; }

        void Load(Server server);
        void Unload(Server server);
    }
}

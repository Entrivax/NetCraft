using NetCraft.Network;

namespace NetCraft.Plugin
{
    public interface IPlugin
    {
        void Load(Server server);
        void Unload(Server server);
    }
}

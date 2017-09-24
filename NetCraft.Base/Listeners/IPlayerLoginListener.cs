using NetCraft.Base.Events;

namespace NetCraft.Base.Listeners
{
    public interface IPlayerLoginListener
    {
        void OnLogin(PlayerLoginEvent playerLoginEvent);
    }
}

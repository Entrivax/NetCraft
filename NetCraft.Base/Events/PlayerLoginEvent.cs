using NetCraft.Base.Entities;
using NetCraft.Plugin;

namespace NetCraft.Base.Events
{
    public class PlayerLoginEvent : ICancellableEvent
    {
        public bool Cancelled { get; private set; }
        public string Reason { get; private set; }
        public Player Player { get; private set; }

        public PlayerLoginEvent(Player player)
        {
            Cancelled = false;
            Player = player;
        }

        public void Cancel(string reason)
        {
            Cancelled = true;
            Reason = reason;
        }
    }
}

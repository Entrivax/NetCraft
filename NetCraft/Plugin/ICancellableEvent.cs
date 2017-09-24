namespace NetCraft.Plugin
{
    public interface ICancellableEvent : IEvent
    {
        bool Cancelled { get; }
        string Reason { get; }

        void Cancel(string reason);
    }
}

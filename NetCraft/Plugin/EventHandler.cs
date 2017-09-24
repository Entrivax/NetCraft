using System;

namespace NetCraft.Plugin
{
    [AttributeUsage(AttributeTargets.Method)]
    public class EventHandler : Attribute
    {
        public Type EventType { get; private set; }

        public EventHandler(Type eventType)
        {
            EventType = eventType;
        }
    }
}

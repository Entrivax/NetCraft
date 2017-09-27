using System;

namespace NetCraft.Plugin
{
    [AttributeUsage(AttributeTargets.Method)]
    public class EventListener : Attribute
    {
        public Type EventType { get; private set; }

        public EventListener(Type eventType)
        {
            EventType = eventType;
        }
    }
}

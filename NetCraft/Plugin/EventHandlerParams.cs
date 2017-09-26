using System;

namespace NetCraft.Plugin
{
    class EventHandlerParams
    {
        public object Instance { get; set; }
        public Action<object, object> Delegate { get; set; }
    }
}

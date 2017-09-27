using System;

namespace NetCraft.Plugin
{
    class EventListenerParams
    {
        public object Instance { get; set; }
        public Action<object, object> Delegate { get; set; }
    }
}

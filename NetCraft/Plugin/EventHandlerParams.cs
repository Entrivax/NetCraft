using System.Reflection;

namespace NetCraft.Plugin
{
    class EventHandlerParams
    {
        public MethodInfo MethodInfo { get; set; }
        public object Instance { get; set; }
    }
}

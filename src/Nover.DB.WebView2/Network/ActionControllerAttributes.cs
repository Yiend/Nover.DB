using System;

namespace Nover.DB.WebView2.Network
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ActionRouteAttribute : Attribute
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class ActionControllerAttribute : Attribute
    {
        public string Name { get; set; }
        public string RoutePath { get; set; }
        public string Description { get; set; }
    }
}
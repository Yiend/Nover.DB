using System;

namespace Nover.DB.WebView2.Network
{
    public class RouteArgument
    {
        public RouteArgument(string propertyName, Type type, int index)
        {
            PropertyName = propertyName;
            Type = type;
            Index = index;
        }

        public string PropertyName { get; set; }
        public Type Type { get; set; }
        public int Index { get; set; }
    }
}

using System.Collections.Generic;

namespace Nover.DB.WebView2.Network
{
    public class RouteDelegate
    {
        public RouteDelegate(dynamic del, IList<RouteArgument> argumentInfos, int argumentCount, bool hasReturnValue)
        {
            Delegate = del;
            RouteArguments = argumentInfos;
            ArgumentCount = argumentCount;
            HasReturnValue = hasReturnValue;
        }

        public dynamic Delegate { get; set; }
        public IList<RouteArgument> RouteArguments { get; set; }
        public int ArgumentCount { get; set; }
        public bool HasReturnValue { get; set; }
    }
}

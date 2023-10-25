using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nover.DB.WebView2.Owin
{

    using AppFunc = Func<IDictionary<string, object>, Task>;

    public interface IOwinPipeline
    {
        AppFunc AppFunc { get; set; }
        string ErrorHandlingPath { get; set; }
        List<OwinRoute> Routes { get; set; }
    }

    public class OwinPipeline : IOwinPipeline
    {
        public OwinPipeline()
        {
            Routes = new List<OwinRoute>();
        }

        public AppFunc AppFunc { get; set; }
        public string ErrorHandlingPath { get; set; }
        public List<OwinRoute> Routes { get; set; }
    }

    public class OwinRoute
    {
        public OwinRoute(string displayName, string routePath, string relativePath = null)
        {
            DisplayName = displayName;
            RoutePath = routePath;
            RelativePath = relativePath;
        }

        public string DisplayName { get; set; }
        public string RoutePath { get; set; }
        public string RelativePath { get; set; }
    }
}

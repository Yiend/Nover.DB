using Nover.DB.WebView2.Configuration;
using Nover.DB.WebView2.Defaults;
using Nover.DB.WebView2.Infrastructure;
using System;

namespace Nover.DB.ReactApp
{
    internal class ReactAppConfig : Configuration
    {
        public ReactAppConfig() : base()
        {
            UrlSchemes.Add(new UrlScheme("http", "dist", "dist", UrlSchemeType.HostToFolder));
            UrlSchemes.Add(new UrlScheme("https://github.com/edgeSharp/EdgeSharp", true, UrlSchemeType.ExternalBrowser));

             var initialUrl = "https://www.bing.com/";
           // var initialUrl = "http://dist/index.html";
            StartUrl = initialUrl;

            // Make borderless
            WindowOptions.MinimumSize = new System.Drawing.Size(800,600);
            WindowOptions.MaximumSize = new System.Drawing.Size(1366, 768);
            WindowOptions.Borderless = true;

            WebView2CreationOptions.AreDefaultContextMenusEnabled = false;
            WebView2CreationOptions.AreDefaultScriptDialogsEnabled = false;
            WebView2CreationOptions.AreBrowserAcceleratorKeysEnabled = false;
            WebView2CreationOptions.IsZoomControlEnabled = false;
            WebView2CreationOptions.IsStatusBarEnabled = false;
            WebView2CreationOptions.AreDevToolsEnabled = false;
            WebView2CreationOptions.IsBuiltInErrorPageEnabled = false;

            CommandLineOptions.Add("disable-web-security");
        }
    }
}
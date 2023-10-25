using System;
using System.Diagnostics;

namespace Nover.DB.WebView2.Network
{
    public static class BrowserLauncher
    {
        public static void Open(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                url = url.Replace("&", "^&");
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
            }
        }
    }
}

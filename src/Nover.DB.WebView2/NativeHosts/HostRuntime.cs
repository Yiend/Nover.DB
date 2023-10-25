using System;
using System.IO;

namespace Nover.DB.WebView2.NativeHosts
{
    public static class HostRuntime
    {
        public static Platform Platform
        {
            get
            {
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.MacOSX:
                        return Platform.MacOSX;

                    case PlatformID.Unix:
                    case (PlatformID)128: 
                        return IsRunningOnMac() ? Platform.MacOSX : Platform.Linux;

                    case PlatformID.Win32NT:
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.WinCE:
                    case PlatformID.Xbox:
                        return Platform.Windows;

                    default:
                        return Platform.NotSupported;
                }
            }
        }

        private static bool IsRunningOnMac()
        {
            try
            {
                var osName = Environment.OSVersion.VersionString;
                if (osName.ToLower().Contains("darwin")) return true;
                if (File.Exists(@"/System/Library/CoreServices/SystemVersion.plist")) return true;
            }
            catch { }

            return false;
        }
    }
}

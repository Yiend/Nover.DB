using System;
using System.IO;

namespace Nover.DB.WebView2.Infrastructure
{
    public static class AppSettingInfo
    {
        public static string GetSettingsFilePath(string appName = "edgesharp", bool onSave = false)
        {
            var fileName = $"{appName}_appsettings.config";
            var appSettingsDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData, Environment.SpecialFolderOption.DoNotVerify), "EdgeSharp");

            if (onSave)
            {
                Directory.CreateDirectory(appSettingsDir);
                if (Directory.Exists(appSettingsDir))
                {
                    return Path.Combine(appSettingsDir, fileName);
                }
            }
            else
            {
                return Path.Combine(appSettingsDir, fileName);
            }
            return null;
        }
    }
}

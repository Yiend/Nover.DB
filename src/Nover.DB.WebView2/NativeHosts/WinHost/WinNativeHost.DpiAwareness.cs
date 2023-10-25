using Nover.DB.WebView2.Configuration;

namespace Nover.DB.WebView2.NativeHosts
{
    public partial class WinNativeHost
    {
        public virtual void SetDpiAwarenessContext()
        {
            SetHighDpiMode(_config.WindowOptions.HighDpiMode);
        }

        public virtual void SetHighDpiMode(HighDpiMode dpiAwareness)
        {
            if (!DpiHelper.FirstParkingWindowCreated)
            {
                DpiHelper.SetProcessDpiAwarenessContext(dpiAwareness);
                DpiHelper.FirstParkingWindowCreated = true;
            }
        }
    }
}

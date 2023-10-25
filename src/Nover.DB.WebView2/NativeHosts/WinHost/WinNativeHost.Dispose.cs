using System;

namespace Nover.DB.WebView2.NativeHosts
{
    public partial class WinNativeHost
    {
        #region Disposal

        ~WinNativeHost()
        {
            Dispose(false);
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            // If there are managed resources
            if (disposing)
            {
            }

            UnregisterComponents();

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}

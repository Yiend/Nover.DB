using System;

namespace Nover.DB.WebView2.Borderless
{
    public partial class BorderlessController : IDisposable
    {
        private bool _disposed = false;

        #region Disposal

        ~BorderlessController()
        {
            Dispose(false);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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

            UninstallDragMouseHook();

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

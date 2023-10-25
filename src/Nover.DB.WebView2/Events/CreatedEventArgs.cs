using System;

namespace Nover.DB.WebView2.Events
{
    public class CreatedEventArgs : EventArgs
    {
        public CreatedEventArgs(IntPtr handle, IntPtr winID)
        {
            Handle = handle;
            WinID = winID;
        }

        public IntPtr Handle { get; }
        public IntPtr WinID { get; }
    }
}

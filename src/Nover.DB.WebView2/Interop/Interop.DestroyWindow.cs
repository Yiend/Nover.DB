using System;
using System.Runtime.InteropServices;

namespace Nover.DB.WebView2
{
    public static partial class Interop
    {
        public static partial class User32
        {
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern BOOL DestroyWindow(IntPtr hWnd);
        }
    }
}

using System;
using System.Runtime.InteropServices;

namespace Nover.DB.WebView2
{
    public static partial class Interop
    {
        public static partial class User32
        {
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern IntPtr GetDC(IntPtr hWnd);

            public static IntPtr GetDC(HandleRef hWnd)
            {
                IntPtr dc = GetDC(hWnd.Handle);
                GC.KeepAlive(hWnd.Wrapper);
                return dc;
            }
        }
    }
}

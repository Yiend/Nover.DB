using System;
using System.Runtime.InteropServices;
using static Nover.DB.WebView2.Interop.UxTheme;

namespace Nover.DB.WebView2
{
    public static partial class Interop
    {
        public static partial class User32
        {
            [DllImport(Libraries.Dwmapi, BestFitMapping = false)]
            public static extern int DwmIsCompositionEnabled(out bool enabled);

            [DllImport(Libraries.Dwmapi, PreserveSig = false)]
            public static extern void DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS pMarInset);

            [DllImport(Libraries.Dwmapi)]
            public static extern void DwmSetWindowAttribute(IntPtr hwnd, DWMWA dwAttribute, ref int pvAttribute, int cbAttribute);
        }
    }
}

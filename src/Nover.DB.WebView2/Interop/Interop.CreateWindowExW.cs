using System;
using System.Runtime.InteropServices;

namespace Nover.DB.WebView2
{
    public static partial class Interop
    {
        public static partial class User32
        {
            [DllImport(Libraries.User32, CharSet = CharSet.Unicode, SetLastError = true)]
            public unsafe static extern IntPtr CreateWindowExW(
                WS_EX dwExStyle,
                char* lpClassName,
                string lpWindowName,
                WS dwStyle,
                int X,
                int Y,
                int nWidth,
                int nHeight,
                IntPtr hWndParent,
                IntPtr hMenu,
                IntPtr hInst,
                [MarshalAs(UnmanagedType.AsAny)] object lpParam);

            public unsafe static IntPtr CreateWindowExW(
                WS_EX dwExStyle,
                string lpClassName,
                string lpWindowName,
                WS dwStyle,
                int X,
                int Y,
                int nWidth,
                int nHeight,
                IntPtr hWndParent,
                IntPtr hMenu,
                IntPtr hInst,
                object lpParam)
            {
                fixed (char* c = lpClassName)
                {
                    return CreateWindowExW(dwExStyle, c, lpWindowName, dwStyle, X, Y, nWidth, nHeight, hWndParent, hMenu, hInst, lpParam);
                }
            }
        }
    }
}

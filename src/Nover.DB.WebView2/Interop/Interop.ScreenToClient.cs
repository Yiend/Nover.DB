﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Nover.DB.WebView2
{
    public static partial class Interop
    {
        public static partial class User32
        {
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern BOOL ScreenToClient(IntPtr hWnd, ref Point pt);

            public static BOOL ScreenToClient(HandleRef hWnd, ref Point lpPoint)
            {
                BOOL result = ScreenToClient(hWnd.Handle, ref lpPoint);
                GC.KeepAlive(hWnd);
                return result;
            }
        }
    }
}

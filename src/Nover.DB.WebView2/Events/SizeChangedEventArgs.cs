using System;

namespace Nover.DB.WebView2.Events
{
    public class SizeChangedEventArgs : EventArgs
    {
        public SizeChangedEventArgs(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Width { get; }
        public int Height { get; }
    }
}

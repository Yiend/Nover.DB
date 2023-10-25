using System;
using System.Drawing;

namespace Nover.DB.WebView2.Borderless
{
    public class MouseMoveEventArgs : EventArgs
    {
        public MouseMoveEventArgs(int xDelta, int yDelta)
        {
            DeltaChangeSize = new Size(xDelta, yDelta);
        }

        public MouseMoveEventArgs(Size deltaSize)
        {
            DeltaChangeSize = deltaSize;
        }

        public Size DeltaChangeSize { get; set; }
    }
}
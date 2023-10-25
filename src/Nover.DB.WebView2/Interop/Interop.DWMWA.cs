
namespace Nover.DB.WebView2
{
    public static partial class Interop
    {
        public static partial class User32
        {
            public enum DWMWA
            {
                NCRENDERING_ENABLED = 1,
                NCRENDERING_POLICY,
                TRANSITIONS_FORCEDISABLED,
                ALLOW_NCPAINT,
                CAPTION_BUTTON_BOUNDS,
                NONCLIENT_RTL_LAYOUT,
                FORCE_ICONIC_REPRESENTATION,
                FLIP3D_POLICY,
                EXTENDED_FRAME_BOUNDS,

                // New to Windows 7:

                HAS_ICONIC_BITMAP,
                DISALLOW_PEEK,
                EXCLUDED_FROM_PEEK,

                // LAST
            }

            public enum DWMNCRP
            {
                USEWINDOWSTYLE,
                DISABLED,
                ENABLED,
                //LAST
            }
        }
    }
}

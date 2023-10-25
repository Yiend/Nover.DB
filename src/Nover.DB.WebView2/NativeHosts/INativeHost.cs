using System;
using System.Drawing;
using Nover.DB.WebView2.Configuration;
using Nover.DB.WebView2.Events;

namespace Nover.DB.WebView2.NativeHosts
{
    public interface INativeHost : IDisposable
    {
        event EventHandler<CreatedEventArgs> Created;
        event EventHandler<MovingEventArgs> Moving;
        event EventHandler<SizeChangedEventArgs> SizeChanged;
        event EventHandler<CloseEventArgs> Close;
        IntPtr Handle { get; }
        void CreateWindow();
        void SetDpiAwarenessContext();
        void SetHighDpiMode(HighDpiMode dpiAwareness);
        void Run();
        Size GetWindowClientSize();
        float GetWindowDpiScale();
        void ResizeBrowser(IntPtr browserWindow, int width, int height);
        void Exit();
        void SetWindowTitle(string title);

        void RegisterComponents(IntPtr hwnd);
        void UnregisterComponents(IntPtr hwnd);

        /// <summary> Gets the current window state Maximised / Normal / Minimised etc. </summary>
        /// <returns> The window state. </returns>
        WindowState GetWindowState();

        /// <summary> Sets window state. Maximise / Minimize / Restore. </summary>
        /// <param name="state"> The state to set. </param>
        /// <returns> True if it succeeds, false if it fails. </returns>
        bool SetWindowState(WindowState state);

        void ToggleFullscreen(IntPtr hWnd);
    }
}

using System;
using Nover.DB.WebView2.Browser;
using Nover.DB.WebView2.Configuration;
using Nover.DB.WebView2.Events;
using Nover.DB.WebView2.NativeHosts;


namespace Nover.DB.WebView2
{
    public partial class WindowController : IWindowController
    {
        protected readonly IConfiguration _config;
        protected readonly IBrowserWindow _window;
        protected readonly INativeHost _nativeHost;

        private IntPtr _nativeHandle;

        public WindowController(IConfiguration config, IBrowserWindow window, INativeHost nativeHost)
        {
            _config = config;
            _window = window;
            _nativeHost = nativeHost;

            _nativeHost.Created += OnWindowCreated;
            _nativeHost.Moving += OnWindowMoving;
            _nativeHost.SizeChanged += OnWindowSizeChanged;
            _nativeHost.Close += OnWindowClosed;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="createdEventArgs"></param>
        public virtual void OnWindowCreated(object sender, CreatedEventArgs createdEventArgs)
        {
            if (createdEventArgs != null)
            {
                _nativeHandle = createdEventArgs.Handle;
                (_window as BrowserWindow)?.Initialize();
                _window.Source = new Uri(_config.StartUrl);
                (_window as BrowserWindow)?.InitCoreWebView2(_nativeHandle);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="sizeChangedEventArgs"></param>
        public virtual void OnWindowSizeChanged(object sender, SizeChangedEventArgs sizeChangedEventArgs)
        {
            if (sizeChangedEventArgs != null)
            {
                (_window as BrowserWindow)?.Resize(sizeChangedEventArgs.Width, sizeChangedEventArgs.Height);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="closeEventArgs"></param>
        public virtual void OnWindowClosed(object sender, CloseEventArgs closeEventArgs)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void OnWindowMoving(object sender, MovingEventArgs e)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual int Run(string[] args)
        {
            return RunInternal(args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        protected virtual int RunInternal(string[] args)
        {
            // Create and show window
            _nativeHost?.CreateWindow();

            // Run message loop
            _nativeHost?.Run();

            return 0;
        }
    }
}

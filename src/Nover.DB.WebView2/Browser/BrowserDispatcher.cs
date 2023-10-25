using System;
using System.Collections.Concurrent;
using Nover.DB.WebView2.Infrastructure;
using static Nover.DB.WebView2.Interop.User32;

namespace Nover.DB.WebView2.Browser
{
    public class BrowserDispatcher : Dispatcher
    {
        protected readonly IntPtr _nativeHostHandle;
        protected ConcurrentQueue<Action> _dispatcherQueue;

        public BrowserDispatcher(IntPtr nativeHostHandle)
        {
            _nativeHostHandle = nativeHostHandle;
            _dispatcherQueue = new ConcurrentQueue<Action>();
        }

        public override void Execute(string actionName)
        {
            actionName = string.IsNullOrWhiteSpace(actionName) ? string.Empty : actionName;
            var browserWindow = ServiceLocator.Current.GetInstance<IBrowserWindow>();
            switch (actionName)
            {
                case DisapatcherExecuteType.Reload:
                    Post(() => browserWindow?.Reload());
                    break;

                case DisapatcherExecuteType.Exit:
                    browserWindow?.Exit();
                    break;
            }
        }

        public override void Post(Action action)
        {
            _dispatcherQueue.Enqueue(action);
            SendMessageW(_nativeHostHandle, WM.APP, IntPtr.Zero, IntPtr.Zero);
        }

        public override void Dispatch()
        {
            Action action;
            while (_dispatcherQueue.TryDequeue(out action))
            {
                action?.Invoke();
            }
        }

        public override void PostMessage(Func<MessageResult> action)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Runtime.InteropServices;

namespace Nover.DB.WebView2
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)]
    public class RequestHandlerServer
    {
        private Delegate _sendRequestCallback;

        public RequestHandlerServer()
        {
            _sendRequestCallback = null;
        }

        public void RegisterSendCallback(IntPtr callback)
        {
            _sendRequestCallback = Marshal.GetDelegateForFunctionPointer(callback, typeof(HostObjectCommon.SendRequestDelegate));
        }

        public string Send(string url, object request = null)
        {
            if (string.IsNullOrWhiteSpace(url) || _sendRequestCallback == null)
            {
                return null;
            }

            object[] args = { url, request };
            var sendResponse = _sendRequestCallback.DynamicInvoke(args);
            return sendResponse?.ToString();
        }
    }
}
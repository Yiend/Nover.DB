using System;
using System.Collections.Generic;

namespace Nover.DB.WebView2.Network
{
    /// <summary>
    /// Script executor
    /// </summary>
    public interface IScriptExecutor
    {
        /// <summary>
        /// Gets list of scripts to execute on DOM "document" created.
        /// </summary>
        IList<string> OnDocumentCreatedScripts { get; }
        /// <summary>
        /// Execute script - to execute script on CoreWebView2.WebMessageReceived Event received.
        /// </summary>
        /// <param name="actionRequest">The request of <see cref="IActionRequest"/> instance.</param>
        /// <param name="executeScriptCallback">The callback method that is executed.</param>
        void ExecuteScript(IActionRequest actionRequest, Action<string> executeScriptCallback);
    }
}

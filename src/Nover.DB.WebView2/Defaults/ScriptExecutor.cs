using System;
using System.Net;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Nover.DB.WebView2.Network;
using Nover.DB.WebView2.Infrastructure;

namespace Nover.DB.WebView2.Defaults
{
    /// <summary>
    /// The default implementation of <see cref="IScriptExecutor"/>.
    /// </summary>
    public class ScriptExecutor : IScriptExecutor
    {
        protected readonly IActionControllerProvider _controllerProvider;
        protected readonly IDataTransferOptions _dataTransferOptions;
        protected readonly ILogger<ScriptExecutor> _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="ScriptExecutor"/>
        /// </summary>
        /// <param name="controllerProvider">The <see cref="IActionControllerProvider"/> instance.</param>
        /// <param name="dataTransferOptions">The <see cref="IDataTransferOptions"/> instance.</param>
        public ScriptExecutor(IActionControllerProvider controllerProvider, IDataTransferOptions dataTransferOptions, ILogger<ScriptExecutor> logger)
        {
            _controllerProvider = controllerProvider;
            _dataTransferOptions = dataTransferOptions;
            _logger = logger;
        }

        /// <inheritdoc />
        public IList<string> OnDocumentCreatedScripts
        {
            get
            {
                var scripts = new List<string>();
                scripts.Add(OnDocumentReadyScriptLoader.PostMessagePromise);
                return scripts;
            }
        }

        /// <inheritdoc />
        public virtual void ExecuteScript(IActionRequest request, Action<string> executeScriptCallback)
        {
            if (request != null && executeScriptCallback != null)
            {
                try
                {
                    bool errorOcurs = true;
                    var responseJson = ExceuteRequest(request, ref errorOcurs);
                    var script = ResponseScript(request.RequestId, responseJson, errorOcurs);
                    executeScriptCallback.Invoke(script);
                }
                catch (Exception exception)
                {
                    _logger?.LogError(exception);
                }
            }
        }

        private string ExceuteRequest(IActionRequest request, ref bool errorOccurs)
        {
            IActionResponse response = _controllerProvider.Execute(request);
            if (response != null)
            {
                errorOccurs = response.StatusCode != HttpStatusCode.OK;
                return _dataTransferOptions.ConvertResponseToJson(response.Content);
            }

            errorOccurs = true;
            return null;
        }

        private string ResponseScript(string requestId, string jsonResponse, bool errorOccurs)
        {
            return errorOccurs
                ? $"window.external.EdgeHandlerErrorResponse('{requestId}', '{jsonResponse}');"
                : $"window.external.EdgeHandlerSuccessResponse('{requestId}', '{jsonResponse}');";
        }
    }
}
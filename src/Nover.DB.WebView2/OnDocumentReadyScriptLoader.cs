using System.IO;
using System.Reflection;

namespace Nover.DB.WebView2
{
    public static class OnDocumentReadyScriptLoader
    {
        private const string PromiseFilePath = "FarAway.WebView2.postMessagePromise.js";

        public static string PostMessagePromise
        {
            get
            {
                var result = default(string);

                var resourcePath = PromiseFilePath;
                using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath))
                {
                    if (resource != null)
                    {
                        using (var reader = new StreamReader(resource))
                        {
                            result = reader.ReadToEnd();
                        }
                    }
                }

                return result;
            }
        }
    }
}

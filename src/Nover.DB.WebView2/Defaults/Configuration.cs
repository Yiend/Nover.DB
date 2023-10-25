using Nover.DB.WebView2.Configuration;
using Nover.DB.WebView2.Infrastructure;
using System.Collections.Generic;
using System.Drawing;

namespace Nover.DB.WebView2.Defaults
{
    /// <summary>
    /// The default implementation of <see cref="IConfiguration"/>.
    /// </summary>
    public class Configuration : IConfiguration
    {
        /// <inheritdoc />
        public string StartUrl { get; set; }
        /// <inheritdoc />
        public string SdkVersion { get; set; }
        /// <inheritdoc />
        public string RuntimeVersion { get; set; }
        /// <inheritdoc />
        public bool DebuggingMode { get; set; }
        /// <inheritdoc />
        public IList<UrlScheme> UrlSchemes { get; set; }
        /// <inheritdoc />
        public IDictionary<string, string> CommandLineArgs { get; set; }
        /// <inheritdoc />
        public IList<string> CommandLineOptions { get; set; }
        /// <inheritdoc />
        public Rectangle BrowserBounds { get; set; }
        /// <inheritdoc />
        public WebView2CreationOptions WebView2CreationOptions { get; set; }
        /// <inheritdoc />
        public IWindowOptions WindowOptions { get; set; }
        /// <inheritdoc />
        public IDictionary<string, object> ExtensionData { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="Configuration"/>.
        /// </summary>
        public Configuration()
        {
            UrlSchemes = new List<UrlScheme>();
            CommandLineArgs = new Dictionary<string, string>();
            CommandLineOptions = new List<string>();
            ExtensionData = new Dictionary<string, object>();
            SdkVersion = VersionInfo.SdkVersion;
            BrowserBounds = new Rectangle(0, 0, 1200, 900);
            WindowOptions = new WindowOptions();
            WebView2CreationOptions = new WebView2CreationOptions();
        }
    }
}
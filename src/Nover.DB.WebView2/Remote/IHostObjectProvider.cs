using System;

namespace Nover.DB.WebView2
{
    /// <summary>
    /// Creates remote host objected to be added to script.
    /// </summary>
    public interface IHostObjectProvider : IDisposable
    {
        /// <summary>
        /// Gets the host object name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the host object.
        /// </summary>
        object HostObject { get; }
    }
}

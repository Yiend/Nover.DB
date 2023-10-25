using System;

namespace Nover.DB.WebView2
{
    /// <summary>
    /// Represents window controller.
    /// </summary>
    public interface IWindowController : IDisposable
    {
         /// <summary>
         /// Runs Win32 EdgeSharp application.
         /// </summary>
         /// <param name="args"></param>
         /// <returns>The success or failure code - [0: success; 1: failure].</returns>
         int Run(string[] args);
    }
}

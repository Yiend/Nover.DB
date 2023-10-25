using Serilog;
using Serilog.Configuration;
using Serilog.Events;
using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;

namespace Nover.DB.ReactApp
{
    public class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
#if DEBUG
             .MinimumLevel.Debug()
#else
                .MinimumLevel.Information()
#endif
             .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
             .Enrich.FromLogContext()
             .WriteTo.Async(ConfigureSerilog)
             .CreateLogger();

            try
            {
                var _application = AbpApplicationFactory.Create<NoverReactAppModule>(options =>
                 {
                     options.UseAutofac();
                     options.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
                 });
                _application.Initialize();
                _application.ServiceProvider.WebViewRun(args);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly!");
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public static void ConfigureSerilog(LoggerSinkConfiguration configuration)
        {
            static string LogFilePath(string LogEvent) => Path.Combine(AppContext.BaseDirectory, "Logs", LogEvent, "log.log");
            string SerilogOutputTemplate = "{NewLine}{NewLine}Date：{Timestamp:yyyy-MM-dd HH:mm:ss.fff}{NewLine}LogLevel：{Level}{NewLine}Message：{Message}{NewLine}{Exception}" + new string('-', 50);

            configuration.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Debug)
                .WriteTo.File(LogFilePath("Debug"), rollingInterval: RollingInterval.Day, outputTemplate: SerilogOutputTemplate))
                .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Information)
                .WriteTo.File(LogFilePath("Information"), rollingInterval: RollingInterval.Day, outputTemplate: SerilogOutputTemplate))
                .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Warning)
                .WriteTo.File(LogFilePath("Warning"), rollingInterval: RollingInterval.Day, outputTemplate: SerilogOutputTemplate))
                .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Error)
                .WriteTo.File(LogFilePath("Error"), rollingInterval: RollingInterval.Day, outputTemplate: SerilogOutputTemplate))
                .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Fatal)
                .WriteTo.File(LogFilePath("Fatal"), rollingInterval: RollingInterval.Day, outputTemplate: SerilogOutputTemplate));
        }
    }
}

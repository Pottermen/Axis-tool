using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace AxisConfigurator.Core.Logging;

/// <summary>
/// Extensions for configuring Serilog logging
/// </summary>
public static class LoggingExtensions
{
    /// <summary>
    /// Configures Serilog for the application
    /// </summary>
    /// <param name="builder">Host builder</param>
    /// <returns>Host builder for chaining</returns>
    public static IHostBuilder UseSerilogLogging(this IHostBuilder builder)
    {
        return builder.UseSerilog((context, configuration) =>
        {
            configuration
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console(
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext}: {Message:lj}{NewLine}{Exception}")
                .WriteTo.File(
                    path: "logs/app-.log",
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 7,
                    outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {Level:u3}] {SourceContext}: {Message:lj}{NewLine}{Exception}");

            // TODO: Add secret redaction for sensitive information like passwords, API keys
            // configuration.Filter.ByExcluding(logEvent => 
            //     logEvent.Properties.ContainsKey("SensitiveData"));
        });
    }
}
using Microsoft.Extensions.DependencyInjection;
using ProjetoCompeticao.Extensions.Logs.Entities;
using ProjetoCompeticao.Extensions.Logs.Services;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Compact;

namespace ProjetoCompeticao.Extensions.Logs.Configurations
{
    public static class LogExtensions
    {
        public static Logger ConfigureStructuralLog()
        {
            return new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Error)
            .Enrich.FromLogContext()
            .Filter.ByExcluding(c => c.Properties.Any(p => p.Value.ToString().Contains("healthcheck")))
            .Filter.ByExcluding(c => c.Properties.Any(p => p.Value.ToString().Contains("healthcheck-ui")))
            .Filter.ByExcluding(c => c.Properties.Any(p => p.Key.ToString().Contains("HealthChecksDb")))
            .Filter.ByExcluding(c => c.Properties.Any(p => p.Key.ToString().Contains("HealthChecksUI")))
            .Filter.ByExcluding(c => c.Properties.Any(p => p.Key.ToString().Contains("healthchecks-data-ui")))
            .Filter.ByExcluding(c => c.Properties.Any(p => p.Key.ToString().Contains("swagger")))
            .WriteTo.Console(new CompactJsonFormatter())
            .CreateLogger();
        }

        public static IServiceCollection AddStructuraLog(this IServiceCollection services)
        {
            services.AddSingleton<ILogServices, LogServices>();
            services.AddSingleton<LogData>();

            return services;
        }
    }
}

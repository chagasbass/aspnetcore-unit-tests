using Microsoft.Extensions.DependencyInjection;
using ProjetoCompeticao.Extensions.Logs.Configurations;
using ProjetoCompeticao.Extensions.Notifications;

namespace ProjetoCompeticao.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection SolveStructuralAppDependencyInjection(this IServiceCollection services)
        {
            services.AddStructuraLog();
            services.AddNotificationControl();

            return services;
        }
    }
}

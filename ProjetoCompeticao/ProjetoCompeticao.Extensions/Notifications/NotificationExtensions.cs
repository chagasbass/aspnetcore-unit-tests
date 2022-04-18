using Microsoft.Extensions.DependencyInjection;
using ProjetoCompeticao.Shared.Notifications;

namespace ProjetoCompeticao.Extensions.Notifications
{
    public static class NotificationExtensions
    {
        public static IServiceCollection AddNotificationControl(this IServiceCollection services)
        {
            services.AddSingleton<INotificationServices, NotificationServices>();
            return services;
        }
    }
}

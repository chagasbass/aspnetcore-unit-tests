using Microsoft.Extensions.DependencyInjection;
using ProjetoCompeticao.Infra.Integrations.PoliciesExtensions;

namespace ProjetoCompeticao.Infra.Integrations.Extensions
{
    public static class IntegrationExtensions
    {
        public static IServiceCollection AddHttpClientResilience(this IServiceCollection services)
        {
            services.AddHttpClient("projeto-competicao")
                    .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                    .AddPolicyHandler(RetryPolicyExtensions.GetRetryPolicy());

            return services;
        }
    }
}

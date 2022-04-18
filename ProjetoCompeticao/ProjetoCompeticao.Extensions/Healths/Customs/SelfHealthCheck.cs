using Microsoft.Extensions.Diagnostics.HealthChecks;
using ProjetoCompeticao.Extensions.Health.Entities;


namespace ProjetoCompeticao.Extensions.Health.Customs
{
    /// <summary>
    /// Healthcheck customizado para monitoramento da própria aplicação
    /// </summary>
    public class SelfHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new HealthCheckResult(
                HealthStatus.Healthy,
                description: HealthNames.SelfDescription));
        }
    }
}

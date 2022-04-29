using Polly;
using Polly.Extensions.Http;

namespace ProjetoCompeticao.Infra.Integrations.PoliciesExtensions
{
    public static class RetryPolicyExtensions
    {
        //TODO Refatorar para trazer os dados do appsettings

        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
    }
}

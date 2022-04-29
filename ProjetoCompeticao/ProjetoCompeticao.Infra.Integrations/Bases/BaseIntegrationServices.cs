using Microsoft.Extensions.Options;
using ProjetoCompeticao.Shared.Configurations;

namespace ProjetoCompeticao.Infra.Integrations.Bases
{
    public abstract class BaseIntegrationServices
    {
        BaseConfigurationOptions _options;
        protected BaseIntegrationServices(IOptionsMonitor<BaseConfigurationOptions> options)
        {
            _options = options.CurrentValue;
        }

        public HttpRequestMessage CreateRequest(string cep)
        {
            //viacep.com.br/ws/24130110/json/
            var cepWithoutMask = cep.Replace("-", string.Empty);

            var url = _options.CepServiceUrl.Replace("my_cep", cepWithoutMask);

            return new HttpRequestMessage(HttpMethod.Get, url);
        }
    }
}

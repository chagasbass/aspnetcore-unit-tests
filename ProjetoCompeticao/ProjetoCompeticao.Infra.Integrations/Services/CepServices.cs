using Microsoft.Extensions.Options;
using ProjetoCompeticao.Infra.Integrations.Bases;
using ProjetoCompeticao.Infra.Integrations.Contracts;
using ProjetoCompeticao.Infra.Integrations.Models;
using ProjetoCompeticao.Shared.Configurations;
using System.Text.Json;

namespace ProjetoCompeticao.Infra.Integrations.Services
{
    public class CepServices : BaseIntegrationServices, ICepServices
    {
        private readonly BaseConfigurationOptions _baseConfigurationOptions;
        private readonly IHttpClientFactory _httpClient;

        public CepServices(IHttpClientFactory httpClient,
                                      IOptionsMonitor<BaseConfigurationOptions> options) : base(options)
        {
            _httpClient = httpClient;
            _baseConfigurationOptions = options.CurrentValue;
        }

        public async Task<EnderecoModel> RecuperarEnderecoPorCepAsync(string cep)
        {
            var externalClient = _httpClient.CreateClient();

            var addressRequest = CreateRequest(cep);

            var addressQuery = new EnderecoModel();

            var addressResponse = await externalClient.SendAsync(addressRequest);

            if (addressResponse.IsSuccessStatusCode)
            {
                var response = await addressResponse.Content.ReadAsStringAsync();

                addressQuery = JsonSerializer.Deserialize<EnderecoModel>(response);
            }

            return addressQuery;
        }
    }
}

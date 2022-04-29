using ProjetoCompeticao.Infra.Integrations.Models;

namespace ProjetoCompeticao.Infra.Integrations.Contracts
{
    public interface ICepServices
    {
        Task<EnderecoModel> RecuperarEnderecoPorCepAsync(string cep);
    }
}

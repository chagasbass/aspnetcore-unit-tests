using ProjetoCompeticao.Shared.Entities;

namespace ProjetoCompeticao.Application.Academias.Contracts
{
    public interface IAcademiaEnderecoApplicationServices
    {
        public Task<ICommandResult> BuscarEnderecoPorCepAsync(string cep);
    }
}

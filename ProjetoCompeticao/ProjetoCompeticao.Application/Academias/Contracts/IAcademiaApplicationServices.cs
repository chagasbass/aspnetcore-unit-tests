using ProjetoCompeticao.Application.Academias.Dtos;
using ProjetoCompeticao.Shared.Entities;

namespace ProjetoCompeticao.Application.Academias.Contracts
{
    public interface IAcademiaApplicationServices
    {
        Task<ICommandResult> AtualizarAcademiaAsync(AtualizarAcademiaDto atualizarAcademiaDto);
        Task<ICommandResult> ExcluirAcademiaAsync(Guid id);
        Task<ICommandResult> InserirAcademiaAsync(InserirAcademiaDto inserirAcademiaDto);
        Task<ICommandResult> ListarAcademiasAsync(Guid id);
        Task<ICommandResult> ListarAcademiasAsync(string nome);
        Task<ICommandResult> ListarAcademiasAsync(FiltroAcademiaDto filtroAcademiaDto);
    }
}

using ProjetoCompeticao.Domain.Academias.Entities;

namespace ProjetoCompeticao.Domain.Academias.Repositories.Read
{
    public interface IReadAcademiaRepository
    {
        Task<Academia> ListarAcademiasAsync(Guid id);
        Task<Academia> ListarAcademiasAsync(string nome);
        Task<AcademiaPagedResults> ListarAcademiasAsync(int pagina = 1, int tamanhoDaPagina = 10);
    }
}

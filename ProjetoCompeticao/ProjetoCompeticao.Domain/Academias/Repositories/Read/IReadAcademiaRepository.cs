using ProjetoCompeticao.Domain.Academias.Entities;
using ProjetoCompeticao.Shared.Entities;

namespace ProjetoCompeticao.Domain.Academias.Repositories.Read
{
    public interface IReadAcademiaRepository
    {
        Task<Academia> ListarAcademiasAsync(Guid id);
        Task<Academia> ListarAcademiasAsync(string nome);
        PagedResults<Academia> ListarAcademias(int pagina = 1, int tamanhoDaPagina = 10);
    }
}

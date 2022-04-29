using ProjetoCompeticao.Domain.Academias.Entities;

namespace ProjetoCompeticao.Domain.Academias.Repositories.Write
{
    public interface IWriteAcademiaRepository
    {
        Task<Academia> AtualizarAcademiaAsync(Academia academia);
        Task ExcluirAcademiaAsync(Academia academia);
        Task<Academia> InserirAcademiaAsync(Academia academia);
    }
}

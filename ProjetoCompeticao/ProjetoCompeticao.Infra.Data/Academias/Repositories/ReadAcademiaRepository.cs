using Microsoft.EntityFrameworkCore;
using ProjetoCompeticao.Domain.Academias.Entities;
using ProjetoCompeticao.Domain.Academias.Repositories.Read;
using ProjetoCompeticao.Infra.Data.DataContexts;
using ProjetoCompeticao.Shared.Entities;
using ProjetoCompeticao.Shared.Extensions;

namespace ProjetoCompeticao.Infra.Data.Academias.Repositories
{
    public class ReadAcademiaRepository : IReadAcademiaRepository
    {
        private readonly DataContext _context;

        public ReadAcademiaRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Academia> ListarAcademiasAsync(Guid id)
        {
            var academia = await _context.Academias.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            return academia;
        }

        public async Task<Academia> ListarAcademiasAsync(string nome)
        {
            var academia = await _context.Academias.AsNoTracking().FirstOrDefaultAsync(x => x.Nome.ToLower().Equals(nome.ToLower().Trim()));

            return academia;
        }

        public PagedResults<Academia> ListarAcademias(int pagina = 1, int tamanhoDaPagina = 10)
        {
            var academias = _context.Academias.AsNoTracking().GetPaged(pagina, tamanhoDaPagina);

            return academias;
        }
    }
}

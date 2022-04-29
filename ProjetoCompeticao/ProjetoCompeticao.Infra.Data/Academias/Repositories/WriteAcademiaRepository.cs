using ProjetoCompeticao.Domain.Academias.Entities;
using ProjetoCompeticao.Domain.Academias.Repositories.Write;
using ProjetoCompeticao.Infra.Data.DataContexts;

namespace ProjetoCompeticao.Infra.Data.Academias.Repositories
{
    public class WriteAcademiaRepository : IWriteAcademiaRepository
    {
        private readonly DataContext _context;
        private readonly List<Academia> _academias;

        public WriteAcademiaRepository(DataContext context)
        {
            _context = context;

            _academias = new List<Academia>();
        }

        public async Task<Academia> AtualizarAcademiaAsync(Academia academia)
        {
            _context.Academias.Update(academia);
            await _context.SaveChangesAsync();

            return academia;
        }

        public async Task ExcluirAcademiaAsync(Academia academia)
        {
            _context.Academias.Remove(academia);
            await _context.SaveChangesAsync();
        }

        public async Task<Academia> InserirAcademiaAsync(Academia academia)
        {
            await _context.Academias.AddAsync(academia);
            await _context.SaveChangesAsync();

            return academia;
        }
    }
}

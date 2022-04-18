using Dapper;
using ProjetoCompeticao.Domain.Academias.Entities;
using ProjetoCompeticao.Domain.Academias.Repositories.Write;
using ProjetoCompeticao.Infra.Data.Academias.QueryHelpers;
using ProjetoCompeticao.Infra.Data.DataContexts;
using Z.Dapper.Plus;

namespace ProjetoCompeticao.Infra.Data.Academias.Repositories
{
    public class WriteAcademiaRepository : IWriteAcademiaRepository
    {
        private readonly DataContext _context;
        private readonly List<Academia> _academias;

        public WriteAcademiaRepository(DataContext context)
        {
            _context = context;

            Dapper.SqlMapper.Settings.CommandTimeout = 0;

            _academias = new List<Academia>();

            AcademiaQueryHelper.GerarMapeamentoDeAcademia();
        }

        public async Task<Academia> AtualizarAcademiaAsync(Academia academia)
        {
            using var connection = _context.AbrirConexao();

            _academias.Add(academia);

            await connection.BulkActionAsync(x => x.BulkUpdate(AcademiaQueryHelper.MappingName, _academias));

            return academia;
        }

        public async Task ExcluirAcademiaAsync(Guid id)
        {
            using var connection = _context.AbrirConexao();

            var parametter = new { ID = id };

            await connection.ExecuteAsync(AcademiaQueryHelper.ExcluirAcademia(), parametter);
        }

        public async Task<Academia> InserirAcademiaAsync(Academia academia)
        {
            using var connection = _context.AbrirConexao();

            _academias.Add(academia);

            await connection.BulkActionAsync(x => x.BulkInsert(AcademiaQueryHelper.MappingName, _academias));

            return academia;
        }
    }
}

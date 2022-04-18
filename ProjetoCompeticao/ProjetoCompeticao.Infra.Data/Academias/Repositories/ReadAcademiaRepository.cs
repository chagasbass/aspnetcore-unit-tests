using Dapper;
using ProjetoCompeticao.Domain.Academias.Entities;
using ProjetoCompeticao.Domain.Academias.Repositories.Read;
using ProjetoCompeticao.Infra.Data.Academias.QueryHelpers;
using ProjetoCompeticao.Infra.Data.DataContexts;

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
            using var connection = _context.AbrirConexao();

            var parametter = new { ID = id };

            var academia = await connection.QueryFirstAsync<Academia>(AcademiaQueryHelper.ListarAcademiaPorId(), parametter);

            return academia;
        }

        public async Task<Academia> ListarAcademiasAsync(string nome)
        {
            using var connection = _context.AbrirConexao();

            var parametter = new { NOME = nome };

            var academia = await connection.QueryFirstAsync<Academia>(AcademiaQueryHelper.ListarAcademiaPorNome(), parametter);

            return academia;
        }

        public async Task<AcademiaPagedResults> ListarAcademiasAsync(int pagina = 1, int tamanhoDaPagina = 10)
        {
            using var connection = _context.AbrirConexao();

            var parametter = new { Offset = (pagina - 1) * tamanhoDaPagina, TAMANHO_PAGINA = tamanhoDaPagina };

            var academias = await connection.QueryAsync<Academia>(AcademiaQueryHelper.ListarAcademiaComPaginacao(), parametter);

            var results = new AcademiaPagedResults(academias, academias.Count());

            return results;
        }
    }
}

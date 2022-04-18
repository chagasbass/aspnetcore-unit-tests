using ProjetoCompeticao.Domain.Academias.Entities;
using System.Text;
using Z.Dapper.Plus;

namespace ProjetoCompeticao.Infra.Data.Academias.QueryHelpers
{
    public static class AcademiaQueryHelper
    {
        public static readonly string MappingName = "AcademiaMappging";

        public static string ListarAcademiaComPaginacao()
        {
            var query = new StringBuilder();
            query.AppendLine(" SELECT ID, NOME, ENDERECO, ATIVO FROM ACADEMIAS");
            query.AppendLine(" WHERE ATIVO = 1");
            query.AppendLine(" ORDER BY NOME");
            query.AppendLine(" OFFSET @Offset ROWS");
            query.AppendLine(" FETCH NEXT @PageSize ROWS ONLY");

            return query.ToString();
        }

        public static string ListarAcademiaPorNome()
        {
            var query = new StringBuilder();
            query.AppendLine(" SELECT ID, NOME, ENDERECO, ATIVO FROM ACADEMIAS");
            query.AppendLine(" WHERE NOME = @NOME AND ATIVO = 1 ");

            return query.ToString();
        }

        public static string ListarAcademiaPorId()
        {
            var query = new StringBuilder();
            query.AppendLine(" SELECT ID, NOME, ENDERECO, ATIVO FROM ACADEMIAS");
            query.AppendLine(" WHERE ID = @ID ");

            return query.ToString();
        }

        public static string ExcluirAcademia()
        {
            var query = new StringBuilder();
            query.AppendLine(" UPDATE ACADEMIAS SET ATIVO = 1 WHERE ID= @ID");

            return query.ToString();
        }

        public static void GerarMapeamentoDeAcademia()
        {
            DapperPlusManager.Entity<Academia>(MappingName)
                             .Table("ACADEMIAS")
                             .Map(x => x.Id, "Id")
                             .Map(x => x.Nome, "NOME")
                             .Map(x => x.Endereco, "ENDERECO")
                             .Map(x => x.Ativo, "ATIVO");
        }
    }
}

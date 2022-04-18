using ProjetoCompeticao.Shared.Entities;

namespace ProjetoCompeticao.Domain.Academias.Entities
{
    public class AcademiaPagedResults : PagedResults<Academia>
    {
        public AcademiaPagedResults(IEnumerable<Academia> results, int totalCount) : base(results, totalCount)
        {
        }
    }
}

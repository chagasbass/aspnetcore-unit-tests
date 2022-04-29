using ProjetoCompeticao.Shared.Entities.Bases;

namespace ProjetoCompeticao.Shared.Entities
{
    public class PagedResults<T> : BasePageResult where T : class
    {
        public IList<T> Results { get; set; }

        public PagedResults()
        {
            Results = new List<T>();
        }
    }
}

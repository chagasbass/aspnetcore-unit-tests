namespace ProjetoCompeticao.Shared.Entities
{
    public abstract class PagedResults<T>
    {
        public IEnumerable<T> Results { get; set; }
        public int TotalCount { get; set; }

        public PagedResults(IEnumerable<T> results, int totalCount)
        {
            Results = results;
            TotalCount = totalCount;
        }
    }
}

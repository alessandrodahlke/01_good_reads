namespace GoodReads.Core.Communication
{
    public class PagedResult<T> where T : class
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalResults { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}

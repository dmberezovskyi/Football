namespace Fs.Application.Queries
{
    public class QueryResult<T>
    {
        public int? Total { get; set; }
        public T[] Items { get; set; }
    }
}

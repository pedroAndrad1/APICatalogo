namespace APICatalogo.Domain.Queries
{
    public interface IQueryResponse<T> where T : class
    {
        public IEnumerable<T> QueryResults { get; init; }
        public IQueryMetadata Metadata { get; init; }
    }
}

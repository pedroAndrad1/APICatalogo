using APICatalogo.Domain.Queries;

namespace APICatalogo.Application.Abstractions
{
    public class QueryResponse<T> : IQueryResponse<T> where T : class
    {
        public required IEnumerable<T> QueryResults { get; init; }
        public required IQueryMetadata Metadata { get; init; }
    }
}

namespace APICatalogo.Domain.Queries
{
    public interface IPagedListCustom<T>
    {
        public int CurrentPage { get; init; }
        public int TotalPages { get; }
        public int PageSize { get; init; }
        public int TotalCount { get; init; }
        public bool HasPrevious { get; }
        public bool HasNext { get; }

    }
}

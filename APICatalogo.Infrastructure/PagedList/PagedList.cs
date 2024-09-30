using APICatalogo.Domain.Queries;

namespace APICatalogo.Infrastructure.PagedList
{
    public class PagedListCustom<T> : List<T>, IPagedListCustom<T> where T : class
    {
        public int CurrentPage { get; init; }
        public int TotalPages { get; }
        public int PageSize { get; init; }
        public int TotalCount { get; init; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public PagedListCustom(List<T> items, int currentPage, int pageSize, int totalCount)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling(totalCount / (double)PageSize);

            AddRange(items);
        }

        public static PagedListCustom<T> ToPagedList(IQueryable<T> query, int pageNumber, int pageSize)
        {
            var count = query.Count();
            var items = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PagedListCustom<T>(items, pageNumber, pageSize, count);
        }

    }
}

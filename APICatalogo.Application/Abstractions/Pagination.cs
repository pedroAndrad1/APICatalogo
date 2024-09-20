using APICatalogo.Domain.Queries;

namespace APICatalogo.Application.Abstractions
{
    public class Pagination : IPagination
    {
        private const int maxPageSize = 50;
        private int _pageSize = maxPageSize;
        public int PageNumber { get; set; } = 1;
        public int PageSize { 
            get { 
                return _pageSize; 
            } 
            set
            {
                _pageSize = value > maxPageSize ? maxPageSize : value;
            }
        }
    }
}

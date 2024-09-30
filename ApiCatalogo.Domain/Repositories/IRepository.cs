using APICatalogo.Domain.Queries;
using System.Linq.Expressions;
using X.PagedList;

namespace APICatalogo.Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<X.PagedList.IPagedList<T>> GetAllAsync(IPagination pagination);
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        T? GetById(Guid id);
        T Create(T entity);
        T Update(T entity);
        T Delete(T entity);
    }
}

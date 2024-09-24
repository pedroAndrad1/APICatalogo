using APICatalogo.Domain.Queries;
using System.Linq.Expressions;

namespace APICatalogo.Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        IPagedList<T> GetAll(IPagination pagination);
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        T? GetById(Guid id);
        T Create(T entity);
        T Update(T entity);
        T Delete(T entity);
    }
}

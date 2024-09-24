using APICatalogo.Infrastructure.Context;
using APICatalogo.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using APICatalogo.Domain.Queries;
using APICatalogo.Infrastructure.PagedList;

namespace APICatalogo.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly AppDbContext _context;
        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public IPagedList<T> GetAll(IPagination pagination)
        {
            var items = _context.Set<T>().AsQueryable();
            var pagedItems = PagedList<T>.ToPagedList(items, pagination.PageNumber, pagination.PageSize);

            return pagedItems;
        
        }

        public T? GetById(Guid id)
        {
            return _context.Set<T>().Find(id);
        }
        public DbSet<T> GetSet()
        {
           return _context.Set<T>();
        }

        public T Create(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }

        public T Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return entity;
        }

        public T Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return entity;
        }
    }
}

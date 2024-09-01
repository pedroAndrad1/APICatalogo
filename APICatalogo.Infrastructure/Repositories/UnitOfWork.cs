using APICatalogo.Domain.models;
using APICatalogo.Infrastructure.Context;
using APICatalogo.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICatalogo.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ICategoriaRepository? _categoriaRepository;

        private IProdutoRepository? _produtoReposytory;

        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public ICategoriaRepository CategoriaRepository
        {
            get
            {
                return _categoriaRepository = _categoriaRepository ?? new CategoriaRepository(_context);
            }
        }

        public IProdutoRepository ProdutoRepository
        {
            get
            {
                return _produtoReposytory = _produtoReposytory ?? new ProdutoRepository(_context);
            }
        }

        public async Task CommitAsync(CancellationToken cancellationToken)
        {
           await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}

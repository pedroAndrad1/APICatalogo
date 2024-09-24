using APICatalogo.Domain.models;
using APICatalogo.Infrastructure.Context;
using APICatalogo.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using APICatalogo.Domain.Queries;

namespace APICatalogo.Infrastructure.Repositories
{
    public class ProdutoRepository : Repository<ProdutoModel>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<ProdutoModel> GetProdutosByCategoria(Guid categoriaId, IPagination pagination)
        {
            return GetSet().Where(p => p.CategoriaId == categoriaId).ToList();
        }
    }
}

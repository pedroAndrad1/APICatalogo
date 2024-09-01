using APICatalogo.Domain.models;
using APICatalogo.Domain.Repositories;
using APICatalogo.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICatalogo.Infrastructure.Repositories
{
    public class CategoriaRepository : Repository<CategoriaModel>, ICategoriaRepository
    {
        public CategoriaRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<CategoriaModel> GetCategoriaWithProdutos()
        {
            return GetSet().Include(c => c.Produtos).ToList();
        }
    }
}

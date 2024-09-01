using APICatalogo.Domain.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICatalogo.Infrastructure.Repositories.Abstractions
{
    public interface ICategoriaRepository : IRepository<CategoriaModel>
    {
        IEnumerable<CategoriaModel> GetCategoriaWithProdutos();
    }
}

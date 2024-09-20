using APICatalogo.Domain.models;
using APICatalogo.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICatalogo.Domain.Repositories
{
    public interface IProdutoRepository : IRepository<ProdutoModel>
    {
        IEnumerable<ProdutoModel> GetProdutosByCategoria(Guid categoriaId, IPagination pagination);
    }
}

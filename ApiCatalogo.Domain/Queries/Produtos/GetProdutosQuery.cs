using APICatalogo.Domain.models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICatalogo.Domain.Queries.Produtos
{
    public class GetProdutosQuery : IRequest<IEnumerable<ProdutoModel>>
    {
    }
}

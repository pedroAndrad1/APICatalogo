using APICatalogo.Domain.models;
using APICatalogo.Domain.Queries.Produtos;
using APICatalogo.Infrastructure.Context;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICatalogo.Application.Queries.Produtos
{
    public class GetProdutosQueryHandler : IRequestHandler<GetProdutosQuery, IEnumerable<ProdutoModel>>
    {
        private readonly AppDbContext _context;

        public GetProdutosQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProdutoModel>> Handle(GetProdutosQuery request, CancellationToken cancellationToken)
        {
            var produtos = _context.Produtos.ToList();

            return produtos;
        }
    }
}

﻿using APICatalogo.Domain.models;
using APICatalogo.Infrastructure.Context;
using MediatR;

namespace APICatalogo.Application.Queries.Produtos
{
    public class GetProdutosQuery : IRequest<IEnumerable<ProdutoModel>>
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
}

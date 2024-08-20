using APICatalogo.Domain.models;
using APICatalogo.Infrastructure.Context;
using MediatR;

namespace APICatalogo.Application.Queries.Produtos
{
    public class GetProdutoByIdQuery : IRequest<ProdutoModel>
    {
        public Guid Id { get; init; }

        public class GetProdutoByIdQueryHandler : IRequestHandler<GetProdutoByIdQuery, ProdutoModel>
        {
            private readonly AppDbContext _context;

            public GetProdutoByIdQueryHandler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<ProdutoModel> Handle(GetProdutoByIdQuery request, CancellationToken cancellationToken)
            {
                var produto = _context.Produtos.Find(request.Id);

                return produto;
            }
        }
    }
}

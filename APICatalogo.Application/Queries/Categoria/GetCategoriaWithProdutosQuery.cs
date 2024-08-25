
using APICatalogo.Domain.models;
using APICatalogo.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Application.Queries.Categoria
{
   public class GetCategoriaWithProdutosQuery : IRequest<IEnumerable<CategoriaModel>>
   {
        public class GetCategoriaWithProdutosQueryHandler : IRequestHandler<GetCategoriaWithProdutosQuery, IEnumerable<CategoriaModel>>
        {
            private readonly AppDbContext _context;

            public GetCategoriaWithProdutosQueryHandler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<CategoriaModel>> Handle(GetCategoriaWithProdutosQuery request, CancellationToken cancellationToken)
            {
                var categorias = _context.Categorias.Include(p => p.Produtos).ToList();
                return categorias;
            }
        }
    }
}

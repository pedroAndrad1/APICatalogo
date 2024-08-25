using APICatalogo.Domain.models;
using APICatalogo.Infrastructure.Context;
using MediatR;

namespace APICatalogo.Application.Queries.Categoria
{
    public class GetCategoriaQuery : IRequest<IEnumerable<CategoriaModel>>
    {
        public class GetCategoriaQueryHandler : IRequestHandler<GetCategoriaQuery, IEnumerable<CategoriaModel>>
        {
            private readonly AppDbContext _context;

            public GetCategoriaQueryHandler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<CategoriaModel>> Handle(GetCategoriaQuery request, CancellationToken cancellationToken)
            {
                var categorias = _context.Categorias.ToList();
                return categorias;
            }
        }
    }
}

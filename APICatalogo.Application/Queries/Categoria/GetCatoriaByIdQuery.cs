using APICatalogo.Application.Queries.Produtos;
using APICatalogo.Domain.models;
using APICatalogo.Infrastructure.Context;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICatalogo.Application.Queries.Categoria
{
    public class GetCatoriaByIdQuery : IRequest<CategoriaModel>
    {
        public Guid Id { get; init; }

        public class GetCatoriaByIdQueryHandler : IRequestHandler<GetCatoriaByIdQuery, CategoriaModel>
        {
            private readonly AppDbContext _context;

            public GetCatoriaByIdQueryHandler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<CategoriaModel> Handle(GetCatoriaByIdQuery request, CancellationToken cancellationToken)
            {
                var categoria = _context.Categorias.Find(request.Id);

                return categoria;
            }
        }
    }
}

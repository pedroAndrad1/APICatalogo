using APICatalogo.Application.Abstractions;
using APICatalogo.Domain.models;
using APICatalogo.Infrastructure.Context;
using MediatR;

namespace APICatalogo.Application.Commands.Categoria
{
    public class UpdateCategoriaCommand : CategoriaCommand
    {
        public class UpdateCategoriaCommandHanlder : IRequestHandler<UpdateCategoriaCommand, CategoriaModel>
        {
            private readonly AppDbContext _context;

            public UpdateCategoriaCommandHanlder(AppDbContext context)
            {
                _context = context;
            }

            public async Task<CategoriaModel> Handle(UpdateCategoriaCommand request, CancellationToken cancellationToken)
            {
                var categoriaToBeUpdated = _context.Categorias.Find(request.Id);
                if (categoriaToBeUpdated == null) return null;

                categoriaToBeUpdated.Nome = request.Nome;
                categoriaToBeUpdated.ImageUrl = request.ImageUrl;
                await _context.SaveChangesAsync(cancellationToken);

                return categoriaToBeUpdated;
            }
        }
    }
}

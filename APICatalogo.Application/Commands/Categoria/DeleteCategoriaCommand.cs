using APICatalogo.Domain.models;
using APICatalogo.Infrastructure.Context;
using MediatR;

namespace APICatalogo.Application.Commands.Categoria
{
    public class DeleteCategoriaCommand : IRequest<CategoriaModel>
    {
        public Guid Id { get; init; }

        public class DeleteCategoriaCommandHandler : IRequestHandler<DeleteCategoriaCommand, CategoriaModel>
        {
            private readonly AppDbContext _context;

            public DeleteCategoriaCommandHandler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<CategoriaModel> Handle(DeleteCategoriaCommand request, CancellationToken cancellationToken)
            {
                var categoriaToBeDeleted = _context.Categorias.Find(request.Id);
                if (categoriaToBeDeleted == null) return null;

                _context.Categorias.Remove(categoriaToBeDeleted);
                await _context.SaveChangesAsync(cancellationToken);

                return categoriaToBeDeleted;
            }
        }
    }
}

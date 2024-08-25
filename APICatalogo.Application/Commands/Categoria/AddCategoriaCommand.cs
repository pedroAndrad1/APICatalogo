
using APICatalogo.Application.Abstractions;
using APICatalogo.Domain.models;
using APICatalogo.Infrastructure.Context;
using MediatR;

namespace APICatalogo.Application.Commands.Categoria
{
    public class AddCategoriaCommand : CategoriaCommand
    {
        public class AddCategoriaCommandHandler : IRequestHandler<AddCategoriaCommand, CategoriaModel>
        {
            private readonly AppDbContext _context;

            public AddCategoriaCommandHandler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<CategoriaModel> Handle(AddCategoriaCommand request, CancellationToken cancellationToken)
            {
                var newCategoria = new CategoriaModel
                {
                    Nome = request.Nome,
                    ImageUrl = request.ImageUrl,
                };
                _context.Add(newCategoria);
                await _context.SaveChangesAsync(cancellationToken);

                return newCategoria;

            }
        }
    }
}

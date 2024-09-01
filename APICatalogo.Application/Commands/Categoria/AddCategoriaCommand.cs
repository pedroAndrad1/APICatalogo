
using APICatalogo.Application.Abstractions;
using APICatalogo.Domain.models;
using APICatalogo.Domain.Repositories;
using MediatR;

namespace APICatalogo.Application.Commands.Categoria
{
    public class AddCategoriaCommand : CategoriaCommand
    {
        public class AddCategoriaCommandHandler : IRequestHandler<AddCategoriaCommand, CategoriaModel>
        {
            private readonly IUnitOfWork _unitOfWork;

            public AddCategoriaCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<CategoriaModel> Handle(AddCategoriaCommand request, CancellationToken cancellationToken)
            {
                var newCategoria = new CategoriaModel
                {
                    Nome = request.Nome,
                    ImageUrl = request.ImageUrl,
                };
                _unitOfWork.CategoriaRepository.Create(newCategoria);

                await _unitOfWork.CommitAsync(cancellationToken);

                return newCategoria;

            }
        }
    }
}

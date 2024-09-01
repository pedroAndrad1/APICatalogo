using APICatalogo.Application.Abstractions;
using APICatalogo.Domain.models;
using APICatalogo.Infrastructure.Context;
using APICatalogo.Infrastructure.Repositories.Abstractions;
using MediatR;

namespace APICatalogo.Application.Commands.Categoria
{
    public class UpdateCategoriaCommand : CategoriaCommand
    {
        public class UpdateCategoriaCommandHanlder : IRequestHandler<UpdateCategoriaCommand, CategoriaModel>
        {
            private readonly IUnitOfWork _unitOfWork;

            public UpdateCategoriaCommandHanlder(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<CategoriaModel?> Handle(UpdateCategoriaCommand request, CancellationToken cancellationToken)
            {
                var categoriaToBeUpdated = _unitOfWork.CategoriaRepository.GetById((Guid)request.Id);
                if (categoriaToBeUpdated == null) return null;

                categoriaToBeUpdated.Nome = request.Nome;
                categoriaToBeUpdated.ImageUrl = request.ImageUrl;
                _unitOfWork.CategoriaRepository.Update(categoriaToBeUpdated);
                await _unitOfWork.CommitAsync(cancellationToken);

                return categoriaToBeUpdated;
            }
        }
    }
}

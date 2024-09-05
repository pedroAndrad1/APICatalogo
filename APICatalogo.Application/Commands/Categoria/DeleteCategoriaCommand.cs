using APICatalogo.Application.DTOs;
using APICatalogo.Domain.models;
using APICatalogo.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace APICatalogo.Application.Commands.Categoria
{
    public class DeleteCategoriaCommand : IRequest<CategoriaDTO>
    {
        public Guid Id { get; init; }

        public class DeleteCategoriaCommandHandler : IRequestHandler<DeleteCategoriaCommand, CategoriaDTO>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public DeleteCategoriaCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<CategoriaDTO> Handle(DeleteCategoriaCommand request, CancellationToken cancellationToken)
            {
                var categoriaToBeDeleted = _unitOfWork.CategoriaRepository.GetById(request.Id);
                if (categoriaToBeDeleted == null) return null;

               _unitOfWork.CategoriaRepository.Delete(categoriaToBeDeleted);
                await _unitOfWork.CommitAsync(cancellationToken);

                var categoriaToBeDeletedDTO = _mapper.Map<CategoriaDTO>(categoriaToBeDeleted);

                return categoriaToBeDeletedDTO;
            }
        }
    }
}

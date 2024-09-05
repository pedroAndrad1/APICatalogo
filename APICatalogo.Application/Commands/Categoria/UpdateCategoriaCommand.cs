using APICatalogo.Application.Abstractions;
using APICatalogo.Application.DTOs;
using APICatalogo.Domain.models;
using APICatalogo.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace APICatalogo.Application.Commands.Categoria
{
    public class UpdateCategoriaCommand : CategoriaCommand
    {
        public class UpdateCategoriaCommandHanlder : IRequestHandler<UpdateCategoriaCommand, CategoriaDTO>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public UpdateCategoriaCommandHanlder(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<CategoriaDTO?> Handle(UpdateCategoriaCommand request, CancellationToken cancellationToken)
            {
                var categoriaToBeUpdated = _unitOfWork.CategoriaRepository.GetById((Guid)request.Id);
                if (categoriaToBeUpdated == null) return null;

                categoriaToBeUpdated.Nome = request.Nome;
                categoriaToBeUpdated.ImageUrl = request.ImageUrl;
                _unitOfWork.CategoriaRepository.Update(categoriaToBeUpdated);
                await _unitOfWork.CommitAsync(cancellationToken);

                var categoriaToBeUpdatedDTO = _mapper.Map<CategoriaDTO>(categoriaToBeUpdated);

                return categoriaToBeUpdatedDTO;
            }
        }
    }
}

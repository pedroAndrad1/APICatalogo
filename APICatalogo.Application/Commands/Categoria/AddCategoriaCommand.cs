
using APICatalogo.Application.Abstractions;
using APICatalogo.Application.DTOs;
using APICatalogo.Domain.models;
using APICatalogo.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace APICatalogo.Application.Commands.Categoria
{
    public class AddCategoriaCommand : CategoriaCommand
    {
        public class AddCategoriaCommandHandler : IRequestHandler<AddCategoriaCommand, CategoriaDTO>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public AddCategoriaCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<CategoriaDTO> Handle(AddCategoriaCommand request, CancellationToken cancellationToken)
            {
                var newCategoria = new CategoriaModel
                {
                    Nome = request.Nome,
                    ImageUrl = request.ImageUrl,
                };
                _unitOfWork.CategoriaRepository.Create(newCategoria);

                await _unitOfWork.CommitAsync(cancellationToken);

                var newCategoriaDTO = _mapper.Map<CategoriaDTO>(newCategoria);

                return newCategoriaDTO;

            }
        }
    }
}

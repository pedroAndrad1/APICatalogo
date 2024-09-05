using APICatalogo.Application.DTOs;
using APICatalogo.Application.Queries.Produtos;
using APICatalogo.Domain.models;
using APICatalogo.Domain.Repositories;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICatalogo.Application.Queries.Categoria
{
    public class GetCatoriaByIdQuery : IRequest<CategoriaDTO>
    {
        public Guid Id { get; init; }

        public class GetCatoriaByIdQueryHandler : IRequestHandler<GetCatoriaByIdQuery, CategoriaDTO>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public GetCatoriaByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<CategoriaDTO> Handle(GetCatoriaByIdQuery request, CancellationToken cancellationToken)
            {
                var categoria = _unitOfWork.CategoriaRepository.GetById(request.Id);
                var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);

                return categoriaDTO;
            }
        }
    }
}

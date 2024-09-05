using APICatalogo.Application.DTOs;
using APICatalogo.Domain.models;
using APICatalogo.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace APICatalogo.Application.Queries.Categoria
{
    public class GetCategoriaQuery : IRequest<IEnumerable<CategoriaDTO>>
    {
        public class GetCategoriaQueryHandler : IRequestHandler<GetCategoriaQuery, IEnumerable<CategoriaDTO>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public GetCategoriaQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<IEnumerable<CategoriaDTO>> Handle(GetCategoriaQuery request, CancellationToken cancellationToken)
            {
                var categorias = _unitOfWork.CategoriaRepository.GetAll();
                var categoriasDTO = _mapper.Map<IEnumerable<CategoriaDTO>>(categorias);
                return categoriasDTO;
            }
        }
    }
}

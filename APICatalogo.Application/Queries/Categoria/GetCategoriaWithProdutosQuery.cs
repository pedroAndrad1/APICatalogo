
using APICatalogo.Application.Abstractions;
using APICatalogo.Application.DTOs;
using APICatalogo.Domain.models;
using APICatalogo.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace APICatalogo.Application.Queries.Categoria
{
   public class GetCategoriaWithProdutosQuery : Pagination, IRequest<IEnumerable<CategoriaDTO>>
   {
        public class GetCategoriaWithProdutosQueryHandler : IRequestHandler<GetCategoriaWithProdutosQuery, IEnumerable<CategoriaDTO>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public GetCategoriaWithProdutosQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<IEnumerable<CategoriaDTO>> Handle(GetCategoriaWithProdutosQuery request, CancellationToken cancellationToken)
            {
                var pagination = new Pagination { PageNumber = request.PageNumber, PageSize = request.PageSize };
                var categorias = _unitOfWork.CategoriaRepository.GetAll(pagination);
                var categoriasDTO = _mapper.Map<IEnumerable<CategoriaDTO>>(categorias);
                return categoriasDTO;
            }
        }
    }
}

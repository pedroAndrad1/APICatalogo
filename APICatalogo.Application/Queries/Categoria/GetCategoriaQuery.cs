using APICatalogo.Application.Abstractions;
using APICatalogo.Application.DTOs;
using APICatalogo.Domain.models;
using APICatalogo.Domain.Queries;
using APICatalogo.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace APICatalogo.Application.Queries.Categoria
{
    public class GetCategoriaQuery : Pagination, IRequest<IQueryResponse<CategoriaDTO>>
    {
        public class GetCategoriaQueryHandler : IRequestHandler<GetCategoriaQuery, IQueryResponse<CategoriaDTO>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public GetCategoriaQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<IQueryResponse<CategoriaDTO>> Handle(GetCategoriaQuery request, CancellationToken cancellationToken)
            {
                var pagination = new Pagination { PageNumber = request.PageNumber, PageSize = request.PageSize };
                var categorias = await _unitOfWork.CategoriaRepository.GetAllAsync(pagination);
                var metadata = new QueryMetadata
                {
                    TotalCount = categorias.Count,
                    PageSize = categorias.PageSize,
                    CurrentPage = categorias.PageCount,
                    TotalPages = categorias.TotalItemCount,
                    HasNext = categorias.HasNextPage,
                    HasPrevious = categorias.HasPreviousPage,

                };
                var categoriasDTO = _mapper.Map<IEnumerable<CategoriaDTO>>(categorias);
                var queryResponse = new QueryResponse<CategoriaDTO> { QueryResults = categoriasDTO, Metadata = metadata};

                return queryResponse;
            }
        }
    }
}

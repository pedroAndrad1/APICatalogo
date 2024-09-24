using APICatalogo.Application.Abstractions;
using APICatalogo.Application.DTOs;
using APICatalogo.Domain.Queries;
using APICatalogo.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace APICatalogo.Application.Queries.Produtos
{
    public class GetProdutosQuery : Pagination, IRequest<IQueryResponse<ProdutoDTO>>
    {
        public class GetProdutoQueryHandler : IRequestHandler<GetProdutosQuery, IQueryResponse<ProdutoDTO>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public GetProdutoQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<IQueryResponse<ProdutoDTO>> Handle(GetProdutosQuery request, CancellationToken cancellationToken)
            {
                var pagination = new Pagination { PageNumber = request.PageNumber, PageSize = request.PageSize };
                var produtos = _unitOfWork.ProdutoRepository.GetAll(pagination);
                var metadata = new QueryMetadata
                {
                    TotalCount = produtos.TotalCount,
                    PageSize = produtos.PageSize,
                    CurrentPage = produtos.CurrentPage,
                    TotalPages = produtos.TotalPages,
                    HasNext = produtos.HasNext,
                    HasPrevious = produtos.HasPrevious,

                };
                var produtosDTO = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
                var queryResponse = new QueryResponse<ProdutoDTO> { QueryResults = produtosDTO, Metadata = metadata };

                return queryResponse;
            }
        }
    }
}

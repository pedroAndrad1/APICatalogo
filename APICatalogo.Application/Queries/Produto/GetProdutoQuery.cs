using APICatalogo.Application.DTOs;
using APICatalogo.Domain.models;
using APICatalogo.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace APICatalogo.Application.Queries.Produtos
{
    public class GetProdutosQuery : IRequest<IEnumerable<ProdutoDTO>>
    {
        public class GetProdutoQueryHandler : IRequestHandler<GetProdutosQuery, IEnumerable<ProdutoDTO>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public GetProdutoQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<IEnumerable<ProdutoDTO>> Handle(GetProdutosQuery request, CancellationToken cancellationToken)
            {
                var produtos = _unitOfWork.ProdutoRepository.GetAll();
                var produtosDTO = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

                return produtosDTO;
            }
        }
    }
}

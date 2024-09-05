using APICatalogo.Application.DTOs;
using APICatalogo.Domain.models;
using APICatalogo.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace APICatalogo.Application.Queries.Produtos
{
    public class GetProdutoByIdQuery : IRequest<ProdutoDTO>
    {
        public Guid Id { get; init; }

        public class GetProdutoByIdQueryHandler : IRequestHandler<GetProdutoByIdQuery, ProdutoDTO>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public GetProdutoByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<ProdutoDTO> Handle(GetProdutoByIdQuery request, CancellationToken cancellationToken)
            {
                var produto = _unitOfWork.ProdutoRepository.GetById(request.Id);
                var produtoDTO = _mapper.Map<ProdutoDTO>(produto);

                return produtoDTO;
            }
        }
    }
}

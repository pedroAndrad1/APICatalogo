using APICatalogo.Domain.models;
using APICatalogo.Infrastructure.Context;
using APICatalogo.Infrastructure.Repositories.Abstractions;
using MediatR;

namespace APICatalogo.Application.Queries.Produtos
{
    public class GetProdutosQuery : IRequest<IEnumerable<ProdutoModel>>
    {
        public class GetProdutoQueryHandler : IRequestHandler<GetProdutosQuery, IEnumerable<ProdutoModel>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetProdutoQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<IEnumerable<ProdutoModel>> Handle(GetProdutosQuery request, CancellationToken cancellationToken)
            {
                var produtos = _unitOfWork.ProdutoRepository.GetAll();

                return produtos;
            }
        }
    }
}

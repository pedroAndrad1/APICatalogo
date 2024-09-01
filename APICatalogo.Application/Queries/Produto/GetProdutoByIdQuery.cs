using APICatalogo.Domain.models;
using APICatalogo.Infrastructure.Context;
using APICatalogo.Infrastructure.Repositories.Abstractions;
using MediatR;

namespace APICatalogo.Application.Queries.Produtos
{
    public class GetProdutoByIdQuery : IRequest<ProdutoModel>
    {
        public Guid Id { get; init; }

        public class GetProdutoByIdQueryHandler : IRequestHandler<GetProdutoByIdQuery, ProdutoModel>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetProdutoByIdQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<ProdutoModel> Handle(GetProdutoByIdQuery request, CancellationToken cancellationToken)
            {
                var produto = _unitOfWork.ProdutoRepository.GetById(request.Id);

                return produto;
            }
        }
    }
}

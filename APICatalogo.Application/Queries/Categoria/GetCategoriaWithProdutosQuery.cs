
using APICatalogo.Domain.models;
using APICatalogo.Infrastructure.Context;
using APICatalogo.Infrastructure.Repositories.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Application.Queries.Categoria
{
   public class GetCategoriaWithProdutosQuery : IRequest<IEnumerable<CategoriaModel>>
   {
        public class GetCategoriaWithProdutosQueryHandler : IRequestHandler<GetCategoriaWithProdutosQuery, IEnumerable<CategoriaModel>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetCategoriaWithProdutosQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<IEnumerable<CategoriaModel>> Handle(GetCategoriaWithProdutosQuery request, CancellationToken cancellationToken)
            {
                var categorias = _unitOfWork.CategoriaRepository.GetCategoriaWithProdutos();
                return categorias;
            }
        }
    }
}

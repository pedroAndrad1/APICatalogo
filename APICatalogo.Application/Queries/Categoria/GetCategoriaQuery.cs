using APICatalogo.Domain.models;
using APICatalogo.Domain.Repositories;
using MediatR;

namespace APICatalogo.Application.Queries.Categoria
{
    public class GetCategoriaQuery : IRequest<IEnumerable<CategoriaModel>>
    {
        public class GetCategoriaQueryHandler : IRequestHandler<GetCategoriaQuery, IEnumerable<CategoriaModel>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetCategoriaQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<IEnumerable<CategoriaModel>> Handle(GetCategoriaQuery request, CancellationToken cancellationToken)
            {
                var categorias = _unitOfWork.CategoriaRepository.GetAll();
                return categorias;
            }
        }
    }
}

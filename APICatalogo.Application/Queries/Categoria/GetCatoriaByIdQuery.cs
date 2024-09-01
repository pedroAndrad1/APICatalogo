using APICatalogo.Application.Queries.Produtos;
using APICatalogo.Domain.models;
using APICatalogo.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICatalogo.Application.Queries.Categoria
{
    public class GetCatoriaByIdQuery : IRequest<CategoriaModel>
    {
        public Guid Id { get; init; }

        public class GetCatoriaByIdQueryHandler : IRequestHandler<GetCatoriaByIdQuery, CategoriaModel>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetCatoriaByIdQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<CategoriaModel> Handle(GetCatoriaByIdQuery request, CancellationToken cancellationToken)
            {
                var categoria = _unitOfWork.CategoriaRepository.GetById(request.Id);

                return categoria;
            }
        }
    }
}

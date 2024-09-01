﻿using APICatalogo.Domain.models;
using APICatalogo.Infrastructure.Context;
using APICatalogo.Infrastructure.Repositories;
using APICatalogo.Infrastructure.Repositories.Abstractions;
using MediatR;

namespace APICatalogo.Application.Commands.Categoria
{
    public class DeleteCategoriaCommand : IRequest<CategoriaModel>
    {
        public Guid Id { get; init; }

        public class DeleteCategoriaCommandHandler : IRequestHandler<DeleteCategoriaCommand, CategoriaModel>
        {
            private readonly IUnitOfWork _unitOfWork;

            public DeleteCategoriaCommandHandler(UnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<CategoriaModel> Handle(DeleteCategoriaCommand request, CancellationToken cancellationToken)
            {
                var categoriaToBeDeleted = _unitOfWork.CategoriaRepository.GetById(request.Id);
                if (categoriaToBeDeleted == null) return null;

               _unitOfWork.CategoriaRepository.Delete(categoriaToBeDeleted);
                await _unitOfWork.CommitAsync(cancellationToken);

                return categoriaToBeDeleted;
            }
        }
    }
}

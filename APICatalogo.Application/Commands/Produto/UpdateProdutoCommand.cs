using APICatalogo.Application.Abstractions;
using APICatalogo.Domain.models;
using APICatalogo.Infrastructure.Context;
using APICatalogo.Infrastructure.Repositories.Abstractions;
using MediatR;

namespace APICatalogo.Application.Commands.Produto
{
    public class UpdateProdutoCommand : ProdutoCommand
    {
        public class UpdateProdutoCommandHander : IRequestHandler<UpdateProdutoCommand, ProdutoModel>
        {
            private readonly IUnitOfWork _unitOfWork;

            public UpdateProdutoCommandHander(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<ProdutoModel?> Handle(UpdateProdutoCommand request, CancellationToken cancellationToken)
            {
                var produtoToBeUpdated = _unitOfWork.ProdutoRepository.GetById((Guid)request.Id);
                if (produtoToBeUpdated == null) return null;

                produtoToBeUpdated.Nome = request.Nome;
                produtoToBeUpdated.Descricao = request.Descricao;
                produtoToBeUpdated.PrecoEmCentavos = request.PrecoEmCentavos;
                produtoToBeUpdated.ImageUrl = request.ImageUrl;
                produtoToBeUpdated.Estoque = request.Estoque;
                produtoToBeUpdated.CategoriaId = request.CategoriaId;
                _unitOfWork.ProdutoRepository.Update(produtoToBeUpdated);
                await _unitOfWork.CommitAsync(cancellationToken);

                return produtoToBeUpdated;
            }
        }
    }
}

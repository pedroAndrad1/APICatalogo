using APICatalogo.Application.Abstractions;
using APICatalogo.Domain.models;
using APICatalogo.Domain.Repositories;
using MediatR;

namespace APICatalogo.Application.Commands.Produto
{
    public class AddProdutoCommand : ProdutoCommand
    {
        public class AddProdutoCommandHandler : IRequestHandler<AddProdutoCommand, ProdutoModel>
        {
            private readonly IUnitOfWork _unitOfWork;

            public AddProdutoCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<ProdutoModel> Handle(AddProdutoCommand request, CancellationToken cancellationToken)
            {
                var newProduto = new ProdutoModel
                {
                    Nome = request.Nome,
                    Descricao = request.Descricao,
                    CategoriaId = request.CategoriaId,
                    Estoque = request.Estoque,
                    PrecoEmCentavos = request.PrecoEmCentavos,
                    ImageUrl = request.ImageUrl
                };

                _unitOfWork.ProdutoRepository.Create(newProduto);
                await _unitOfWork.CommitAsync(cancellationToken);

                return newProduto;

            }
        }
    }
}

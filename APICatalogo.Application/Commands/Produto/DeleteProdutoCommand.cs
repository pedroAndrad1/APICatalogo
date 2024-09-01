using APICatalogo.Domain.models;
using APICatalogo.Domain.Repositories;
using MediatR;

namespace APICatalogo.Application.Commands.Produto
{
    public class DeleteProdutoCommand : IRequest<ProdutoModel>
    {
        public Guid Id { get; set; }

        public class DeleteProdutoCommandHanlder : IRequestHandler<DeleteProdutoCommand, ProdutoModel>
        {
            private readonly IUnitOfWork _unitOfWork;

            public DeleteProdutoCommandHanlder(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<ProdutoModel?> Handle(DeleteProdutoCommand request, CancellationToken cancellationToken)
            {
                var produtoToBeDeleted = _unitOfWork.ProdutoRepository.GetById(request.Id);
                if(produtoToBeDeleted == null) return null;
                

                _unitOfWork.ProdutoRepository.Delete(produtoToBeDeleted);
                await _unitOfWork.CommitAsync(cancellationToken);

                return produtoToBeDeleted;

            }
        }
    }
}

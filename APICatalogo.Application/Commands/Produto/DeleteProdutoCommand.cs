using APICatalogo.Domain.models;
using APICatalogo.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Application.Commands.Produto
{
    public class DeleteProdutoCommand : IRequest<ProdutoModel>
    {
        public Guid Id { get; set; }

        public class DeleteProdutoCommandHanlder : IRequestHandler<DeleteProdutoCommand, ProdutoModel>
        {
            private readonly AppDbContext _context;

            public DeleteProdutoCommandHanlder(AppDbContext context)
            {
                _context = context;
            }

            public async Task<ProdutoModel?> Handle(DeleteProdutoCommand request, CancellationToken cancellationToken)
            {
                var produtoToBeDeleted = _context.Produtos.Find(request.Id);
                if(produtoToBeDeleted == null) return null;
                

                _context.Produtos.Remove(produtoToBeDeleted);
                await _context.SaveChangesAsync(cancellationToken);

                return produtoToBeDeleted;

            }
        }
    }
}

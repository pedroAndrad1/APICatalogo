using APICatalogo.Application.Abstractions;
using APICatalogo.Domain.models;
using APICatalogo.Infrastructure.Context;
using MediatR;

namespace APICatalogo.Application.Commands.Produto
{
    public class UpdateProdutoCommand : ProdutoCommand
    {
        public class UpdateProdutoCommandHander : IRequestHandler<UpdateProdutoCommand, ProdutoModel>
        {
            private readonly AppDbContext _context;

            public UpdateProdutoCommandHander(AppDbContext context)
            {
                _context = context;
            }

            public async Task<ProdutoModel?> Handle(UpdateProdutoCommand request, CancellationToken cancellationToken)
            {
                var produtoToBeUpdated = _context.Produtos.Find(request.Id);
                if (produtoToBeUpdated == null) return null;

                produtoToBeUpdated.Nome = request.Nome;
                produtoToBeUpdated.Descricao = request.Descricao;
                produtoToBeUpdated.PrecoEmCentavos = request.PrecoEmCentavos;
                produtoToBeUpdated.ImageUrl = request.ImageUrl;
                produtoToBeUpdated.Estoque = request.Estoque;
                produtoToBeUpdated.CategoriaId = request.CategoriaId;
                await _context.SaveChangesAsync(cancellationToken);

                return produtoToBeUpdated;
            }
        }
    }
}

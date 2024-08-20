using APICatalogo.Application.Abstractions;
using APICatalogo.Domain.models;
using APICatalogo.Infrastructure.Context;
using MediatR;

namespace APICatalogo.Application.Commands.Produto
{
    public class AddProdutoCommand : ProdutoCommand
    {
        public class AddProdutoCommandHandler : IRequestHandler<AddProdutoCommand, ProdutoModel>
        {
            private readonly AppDbContext _context;

            public AddProdutoCommandHandler(AppDbContext context)
            {
                _context = context;
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

                _context.Add(newProduto);
                await _context.SaveChangesAsync(cancellationToken);

                return newProduto;

            }
        }
    }
}

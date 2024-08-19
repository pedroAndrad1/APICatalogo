using APICatalogo.Domain.models;
using MediatR;

namespace APICatalogo.Domain.Queries.Produtos
{
    public class GetProdutoByIdQuery : IRequest<ProdutoModel>
    {
        public Guid Id { get; init; }
    }
}

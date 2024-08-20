using APICatalogo.Domain.models;
using MediatR;

namespace APICatalogo.Application.Abstractions
{
    public class ProdutoCommand : IRequest<ProdutoModel>
    {
        public Guid? Id { get; init; }
        public string Nome { get; init; } = string.Empty;
        public string Descricao { get; init; } = string.Empty;
        public int PrecoEmCentavos { get; init; }
        public string ImageUrl { get; init; } = string.Empty;
        public float Estoque { get; init; }
        public Guid CategoriaId { get; init; }
    }
}

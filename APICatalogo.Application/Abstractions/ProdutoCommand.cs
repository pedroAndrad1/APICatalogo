using APICatalogo.Domain.CustomAttributeValidation;
using APICatalogo.Domain.models;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace APICatalogo.Application.Abstractions
{
    public class ProdutoCommand : IRequest<ProdutoModel>
    {
        public Guid? Id { get; init; }
        [Required]
        [StringLength(80)]
        [PrimeiraLetraMaiuscula]
        public string Nome { get; init; } = string.Empty;
        [Required]
        [StringLength(300)]
        public string Descricao { get; init; } = string.Empty;
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Valor mínimo de um centavo.")]
        public int PrecoEmCentavos { get; init; }
        [Required]
        [StringLength(300)]
        public string ImageUrl { get; init; } = string.Empty;
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Valor mínimo de zero itens em estoque.")]
        public float Estoque { get; init; }
        [Required]
        public Guid CategoriaId { get; init; }
    }
}

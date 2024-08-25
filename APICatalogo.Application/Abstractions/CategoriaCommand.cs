using APICatalogo.Domain.models;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace APICatalogo.Application.Abstractions
{
    public class CategoriaCommand : IRequest<CategoriaModel>
    {
        public Guid? Id { get; set; }
        [Required]
        [StringLength(80)]
        public string Nome { get; set; } = string.Empty;
        [Required]
        [StringLength(300)]
        public string ImageUrl { get; set; } = string.Empty;
        public ICollection<ProdutoModel>? Produtos { get; set; }
    }
}

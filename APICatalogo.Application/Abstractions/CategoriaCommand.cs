using APICatalogo.Application.DTOs;
using APICatalogo.Domain.models;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APICatalogo.Application.Abstractions
{
    public class CategoriaCommand : IRequest<CategoriaDTO>
    {
        public Guid? Id { get; set; }
        [Required]
        [StringLength(80)]
        public string Nome { get; set; } = string.Empty;
        [Required]
        [StringLength(300)]
        public string ImageUrl { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<ProdutoModel>? Produtos { get; set; }
    }
}

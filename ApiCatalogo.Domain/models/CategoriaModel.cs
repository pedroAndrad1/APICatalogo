using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APICatalogo.Domain.models
{
    public record CategoriaModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; init; }
        [Required]
        [StringLength(80)]
        public string Nome { get; init; } = string.Empty;
        [Required]
        [StringLength(300)]
        public string ImageUrl { get; init; } = string.Empty;
        public ICollection<ProdutoModel>? Produtos { get; init; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Created_at { get; init; }
    }
}

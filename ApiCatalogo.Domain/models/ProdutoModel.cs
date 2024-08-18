using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APICatalogo.Domain.models
{
    public record ProdutoModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; init; }
        [Required]
        [StringLength(80)]
        public string Nome { get; init; } = string.Empty;
        [Required]
        [StringLength(300)]
        public string Descricao { get; init; } = string.Empty;
        [Required]
        public int PrecoEmCentavos { get; init; }
        [Required]
        [StringLength(300)]
        public string ImageUrl { get; init; } = string.Empty;
        public float Estoque { get; init; }
        public Guid CategoriaId { get; init; }
        public CategoriaModel? Categoria { get; init; }


        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Created_at { get; init; }
    }
}

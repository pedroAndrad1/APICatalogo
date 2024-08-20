using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APICatalogo.Domain.models
{
    public record CategoriaModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(80)]
        public string Nome { get; set; } = string.Empty;
        [Required]
        [StringLength(300)]
        public string ImageUrl { get; set; } = string.Empty;
        public ICollection<ProdutoModel>? Produtos { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Created_at { get; set; }
    }
}

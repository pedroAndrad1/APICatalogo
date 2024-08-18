using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APICatalogo.Domain.models
{
    public record CategoriaModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; private set; }
        [Required]
        [StringLength(80)]
        public string Nome { get; private set; } = string.Empty;
        [Required]
        [StringLength(300)]
        public string ImageUrl { get; private set; } = string.Empty;
        public ICollection<ProdutoModel>? Produtos { get; private set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Created_at { get; private set; }
    }
}

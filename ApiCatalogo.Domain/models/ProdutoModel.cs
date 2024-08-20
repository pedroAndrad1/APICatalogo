using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APICatalogo.Domain.models
{
    public record ProdutoModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(80)]
        public string Nome { get; set; } = string.Empty;
        [Required]
        [StringLength(300)]
        public string Descricao { get; set; } = string.Empty;
        [Required]
        public int PrecoEmCentavos { get; set; }
        [Required]
        [StringLength(300)]
        public string ImageUrl { get; set; } = string.Empty;
        public float Estoque { get; set; }
        public Guid CategoriaId { get; set; }
        public CategoriaModel? Categoria { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Created_at { get; set; }
    }
}

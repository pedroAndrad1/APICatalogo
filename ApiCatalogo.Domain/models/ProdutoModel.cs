using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APICatalogo.Domain.models
{
    public record ProdutoModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public int PrecoEmCentavos { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public float Estoque { get; set; }
        public Guid CategoriaId { get; set; }


        [JsonIgnore]
        public CategoriaModel? Categoria { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Created_at { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APICatalogo.Domain.models
{
    public record CategoriaModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;


        [JsonIgnore]
        public ICollection<ProdutoModel>? Produtos { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Created_at { get; set; }
    }
}

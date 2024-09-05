using System.ComponentModel.DataAnnotations;


namespace APICatalogo.Application.DTOs
{
    public record CategoriaDTO
    {
        public Guid? Id { get; set; }
        [Required]
        [StringLength(80)]
        public string Nome { get; set; } = string.Empty;
        [Required]
        [StringLength(300)]
        public string ImageUrl { get; set; } = string.Empty;
    }
}

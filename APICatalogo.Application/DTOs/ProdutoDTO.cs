namespace APICatalogo.Application.DTOs
{
    public record ProdutoDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public int PrecoEmCentavos { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public Guid CategoriaId { get; set; }

    }
}

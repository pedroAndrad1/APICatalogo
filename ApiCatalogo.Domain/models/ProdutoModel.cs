namespace ApiCatalogo.Domain.models
{
    public record ProdutoModel
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; } = string.Empty;
        public string Descricao { get; private set; } = string.Empty;
        public int PrecoEmCentavos { get; private set; }
        public string ImageUrl { get; private set; } = string.Empty;
        public float Estoque { get; private set; }
        public DateTime Created_at { get; private set; }
    }
}

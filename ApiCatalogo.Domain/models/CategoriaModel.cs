namespace APICatalogo.Domain.models
{
    public record CategoriaModel
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; } = string.Empty;
        public string ImageUrl { get; private set; } = string.Empty;
    }
}

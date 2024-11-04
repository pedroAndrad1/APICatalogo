namespace APICatalogo.Application.DTOs
{
    public record TokenDTO
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}

namespace APICatalogo.Domain.Identity
{
    public interface IApplicationUser
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}

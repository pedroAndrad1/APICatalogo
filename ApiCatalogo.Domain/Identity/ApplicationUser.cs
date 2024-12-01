using Microsoft.AspNetCore.Identity;

namespace APICatalogo.Domain.Identity
{
    public class ApplicationUser : IdentityUser, IApplicationUser
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}

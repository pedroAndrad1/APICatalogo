using APICatalogo.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICatalogo.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser, IApplicationUser
    {
        public string? RefreshToken { get ; set; }
        public DateTime RefreshTokenExpiryTime { get ; set; }
    }
}

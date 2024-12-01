using APICatalogo.Application.DTOs;
using APICatalogo.Domain.Identity;
using APICatalogo.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace APICatalogo.Application.Commands.Identity
{
    public class RefreshTokenCommand : IRequest<TokenDTO?>
    {
       public TokenDTO? Token { get; set; }

        public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, TokenDTO?>
        {
            private readonly ITokenService _tokenService;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IConfiguration _configuration;

            public RefreshTokenCommandHandler(
                ITokenService tokenService, 
                UserManager<ApplicationUser> userManager, 
                IConfiguration configuration
            )
            {
                _tokenService = tokenService;
                _userManager = userManager;
                _configuration = configuration;
            }

            public async Task<TokenDTO>? Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
            {
                if(request.Token == null) return null;
          

                var principal = _tokenService.GetPrincipalFromExpiredToken(request.Token.AccessToken!, _configuration);
                if(principal == null) return null;

                var user = await _userManager.FindByNameAsync(principal.Identity!.Name!);

                if (
                    user == null ||
                    user.RefreshToken != request.Token.RefreshToken ||
                    user.RefreshTokenExpiryTime <= DateTime.Now
                )
                {
                    return null;
                }

                var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims.ToList(), _configuration);
                var newRefreshToken = _tokenService.GenerateRefreshToken();

                user.RefreshToken = newRefreshToken;
                await _userManager.UpdateAsync(user);

                return new TokenDTO
                {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                    RefreshToken = newRefreshToken,
                    Expiration = newAccessToken.ValidTo.ToLongDateString(),
                };
            }
        }
    }
}

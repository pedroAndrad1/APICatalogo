using APICatalogo.Application.DTOs;
using APICatalogo.Application.Responses;
using APICatalogo.Domain.Identity;
using APICatalogo.Domain.Responses;
using APICatalogo.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace APICatalogo.Application.Commands.Identity
{
    public class LoginCommand : IRequest<IActionResponse<TokenDTO>>
    {
        [Required(ErrorMessage = "Usuário obrigatório")]
        public string? Username {  get; set; }
        [Required(ErrorMessage = "Senha obrigatório")]
        public string? Password { get; set; }

        public class LoginCommandHandler : IRequestHandler<LoginCommand, IActionResponse<TokenDTO>>
        {

            private readonly ITokenService _tokenService;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IConfiguration _configuration;

            public LoginCommandHandler(
                ITokenService tokenService,
                UserManager<ApplicationUser> userManager, 
                IConfiguration configuration
            )
            {
                _tokenService = tokenService;
                _userManager = userManager;
                _configuration = configuration;
            }

            public async Task<IActionResponse<TokenDTO>> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(request.Username!);
                if(user is not null && await _userManager.CheckPasswordAsync(user, request.Password!))
                {
                    var authClaims = await MountClaims(user, request);
                    var token = _tokenService.GenerateAccessToken(authClaims, _configuration);
                    var refreshToken = _tokenService.GenerateRefreshToken();
                    _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInMinutes"], out int refreshValidityInMinutes);

                    user.RefreshToken = refreshToken;
                    user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(refreshValidityInMinutes);
                    await _userManager.UpdateAsync(user);

                    return new ActionResponse<TokenDTO>()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Data = new TokenDTO
                        {
                            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                            RefreshToken = refreshToken,
                            Expiration = token.ValidTo,
                        }
                    };
                }

                return new ActionResponse<TokenDTO>()
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                }; ;
            }

            private async Task<List<Claim>> MountClaims(ApplicationUser user, LoginCommand request)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, request.Username!),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };
                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                return authClaims;
            }
        }
    }
}

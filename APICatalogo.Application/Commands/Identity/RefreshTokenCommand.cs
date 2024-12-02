using APICatalogo.Application.DTOs;
using APICatalogo.Application.Responses;
using APICatalogo.Domain.Identity;
using APICatalogo.Domain.Responses;
using APICatalogo.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace APICatalogo.Application.Commands.Identity
{
    public class RefreshTokenCommand : IRequest<IActionResponse<TokenDTO>>
    {
       public TokenDTO? Token { get; set; }

        public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, IActionResponse<TokenDTO>>
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

            public async Task<IActionResponse<TokenDTO>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
            {
                if(request.Token == null)
                {
                    return new ActionResponse<TokenDTO>()
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                };
          

                var principal = _tokenService.GetPrincipalFromExpiredToken(request.Token.AccessToken!, _configuration);
                if(principal == null)
                {
                    return new ActionResponse<TokenDTO>()
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }

                var user = await _userManager.FindByNameAsync(principal.Identity!.Name!);

                if (
                    user == null ||
                    user.RefreshToken != request.Token.RefreshToken ||
                    user.RefreshTokenExpiryTime <= DateTime.Now
                )
                {
                    return new ActionResponse<TokenDTO>()
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };;
                }

                var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims.ToList(), _configuration);
                var newRefreshToken = _tokenService.GenerateRefreshToken();

                user.RefreshToken = newRefreshToken;
                await _userManager.UpdateAsync(user);

                return new ActionResponse<TokenDTO>()
                {
                    StatusCode = StatusCodes.Status200OK,
                    Data = new TokenDTO
                    {
                        AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                        RefreshToken = newRefreshToken,
                        Expiration = newAccessToken.ValidTo,
                    }
                }; ;
            }
        }
    }
}

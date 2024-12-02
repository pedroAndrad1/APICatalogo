using APICatalogo.Application.Responses;
using APICatalogo.Domain.Identity;
using APICatalogo.Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace APICatalogo.Application.Commands.Identity
{
    public class RegisterCommand : IRequest<IActionResponse<NoResponse>>
    {
        [Required(ErrorMessage = "Usuário obrigatório")]
        public string? Username { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Senha obrigatório")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Senha obrigatório")]
        public string? Password { get; set; }

        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, IActionResponse<NoResponse>>
        {
            private readonly UserManager<ApplicationUser> _userManager;

            public RegisterCommandHandler(
                UserManager<ApplicationUser> userManager
            )
            {
                _userManager = userManager;
            }

            public async Task<IActionResponse<NoResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                var userExists = await _userManager.FindByNameAsync(request.Username!);
                if (userExists != null)
                {
                    return new ActionResponse<NoResponse>()
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                };

                ApplicationUser user = new()
                {
                    Email = request.Email,
                    UserName = request.Username,
                    SecurityStamp = Guid.NewGuid().ToString(),
                };

                var result = await _userManager.CreateAsync(user, request.Password!);

                if (!result.Succeeded)
                {
                    return new ActionResponse<NoResponse>()
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                }

                return new ActionResponse<NoResponse>()
                {
                    StatusCode = StatusCodes.Status201Created
                }; ;
            }
        }
    }
}

using APICatalogo.Domain.Identity;
using APICatalogo.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;

namespace APICatalogo.Application.Commands.Identity
{
    public class RegisterCommand : IRequest<bool>
    {
        [Required(ErrorMessage = "Usuário obrigatório")]
        public string? Username { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Senha obrigatório")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Senha obrigatório")]
        public string? Password { get; set; }

        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, bool>
        {
            private readonly UserManager<ApplicationUser> _userManager;

            public RegisterCommandHandler(
                UserManager<ApplicationUser> userManager
            )
            {
                _userManager = userManager;
            }

            public async Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                var userExists = await _userManager.FindByNameAsync(request.Username!);
                if (userExists != null) return false;

                ApplicationUser user = new()
                {
                    Email = request.Email,
                    UserName = request.Username,
                    SecurityStamp = Guid.NewGuid().ToString(),
                };

                var result = await _userManager.CreateAsync(user, request.Password!);

                if (!result.Succeeded)
                {
                    return false;
                }

                return true;
            }
        }
    }
}

using APICatalogo.Domain.Identity;
using APICatalogo.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace APICatalogo.Application.Commands.Identity
{
    public class RevokeCommand : IRequest<bool>
    {
        public string Username { get; set; } = string.Empty;

        public class RevokeCommandHandler : IRequestHandler<RevokeCommand, bool>
        {

            private readonly UserManager<ApplicationUser> _userManager;

            public RevokeCommandHandler(
                UserManager<ApplicationUser> userManager
            )
            {
                _userManager = userManager;
            }

            public async Task<bool> Handle(RevokeCommand request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(request.Username);
                if (user == null) return false;

                user.RefreshToken = null;
                await _userManager.UpdateAsync(user);

                return true;


            }
        }
    }
}

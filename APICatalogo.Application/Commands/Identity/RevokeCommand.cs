using APICatalogo.Application.Responses;
using APICatalogo.Domain.Identity;
using APICatalogo.Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace APICatalogo.Application.Commands.Identity
{
    public class RevokeCommand : IRequest<IActionResponse<NoResponse>>
    {
        public string Username { get; set; } = string.Empty;

        public class RevokeCommandHandler : IRequestHandler<RevokeCommand, IActionResponse<NoResponse>>
        {

            private readonly UserManager<ApplicationUser> _userManager;

            public RevokeCommandHandler(
                UserManager<ApplicationUser> userManager
            )
            {
                _userManager = userManager;
            }

            public async Task<IActionResponse<NoResponse>> Handle(RevokeCommand request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(request.Username);
                if (user == null)
                {
                    return new ActionResponse<NoResponse>()
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                };

                user.RefreshToken = null;
                await _userManager.UpdateAsync(user);

                return new ActionResponse<NoResponse>()
                {
                    StatusCode = StatusCodes.Status200OK
                };


            }
        }
    }
}

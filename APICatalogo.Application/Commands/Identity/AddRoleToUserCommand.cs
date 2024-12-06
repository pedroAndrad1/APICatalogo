using APICatalogo.Application.Responses;
using APICatalogo.Domain.Identity;
using APICatalogo.Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace APICatalogo.Application.Commands.Identity
{
    public class AddRoleToUserCommand : IRequest<IActionResponse<string>>
    {
        public required string UserEmail { get; set; }
        public required string RoleName { get; set; }

        public class AddRoleToUserHandler : IRequestHandler<AddRoleToUserCommand, IActionResponse<string>>
        {
            private readonly UserManager<ApplicationUser> _userManager;

            public AddRoleToUserHandler(UserManager<ApplicationUser> userManager)
            {
                _userManager = userManager;
            }

            public async Task<IActionResponse<string>> Handle(AddRoleToUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.UserEmail);
                if (user == null)
                {
                    return new ActionResponse<string>()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Data = $"User {request.UserEmail} não encontrado."
                    };
                }

                var result = await _userManager.AddToRoleAsync(user, request.RoleName);
                if(!result.Succeeded)
                {
                    return new ActionResponse<string>()
                    {
                        StatusCode = StatusCodes.Status500InternalServerError,
                        Data = result.Errors.ToString()
                    };
                }

                return new ActionResponse<string>()
                {
                    StatusCode = StatusCodes.Status200OK,
                    Data = $"User {request.UserEmail} adicionado a role {request.RoleName}."
                };
            }
        }
    }
}

using APICatalogo.Application.Responses;
using APICatalogo.Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace APICatalogo.Application.Commands.Identity
{
    public class AddRoleCommand : IRequest<IActionResponse<string>>
    {
        public required string RoleName { get; set; }

        public class AddRoleCommandHandler : IRequestHandler<AddRoleCommand, IActionResponse<string>>
        {
            private readonly RoleManager<IdentityRole> _roleManager;

            public AddRoleCommandHandler(RoleManager<IdentityRole> roleManager)
            {
                _roleManager = roleManager;
            }

            public async Task<IActionResponse<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(request.RoleName));
                if(result.Succeeded)
                {
                    return new ActionResponse<string>()
                    {
                        StatusCode = StatusCodes.Status201Created,
                        Data = $"Role {request.RoleName} criada com sucesso."
                    };
                }

                return new ActionResponse<string>()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Data = result.Errors.ToString()
                };
            }
        }
    }
}

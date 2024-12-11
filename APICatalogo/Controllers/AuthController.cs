using APICatalogo.Application.Commands.Identity;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace APICatalogo.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {

        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var actionResponse = await _mediator.Send(command);
            return StatusCode(actionResponse.StatusCode, actionResponse.Data);
        }

        [HttpPost]
        [Route("resgister")]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            var actionResponse = await _mediator.Send(command);
            return StatusCode(actionResponse.StatusCode, actionResponse.Data);
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenCommand command)
        {
            var actionResponse = await _mediator.Send(command);
            return StatusCode(actionResponse.StatusCode, actionResponse.Data);
        }
        /// <summary>
        /// Anula o refresh token de um usuário
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [Authorize(policy: "Admin")]
        [HttpPost]
        [Route("revoke/{username}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Revoke(string username)
        {
            var command = new RevokeCommand() { Username = username };
            var actionResponse = await _mediator.Send(command);
            return StatusCode(actionResponse.StatusCode, actionResponse.Data);
        }

        [Authorize(policy: "Admin")]
        [HttpPost]
        [Route("add-role")]
        public async Task<IActionResult> AddRole(AddRoleCommand command)
        {
            var actionResponse = await _mediator.Send(command);
            return StatusCode(actionResponse.StatusCode, actionResponse.Data);
        }

        [Authorize(policy: "Admin")]
        [HttpPost]
        [Route("add-role-to-user")]
        public async Task<IActionResult> AddRoleToUser(AddRoleToUserCommand command)
        {
            var actionResponse = await _mediator.Send(command);
            return StatusCode(actionResponse.StatusCode, actionResponse.Data);
        }

    }
}

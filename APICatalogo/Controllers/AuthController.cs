using APICatalogo.Application.Commands.Identity;
using APICatalogo.Application.Queries.Categoria;
using APICatalogo.Application.Responses;
using APICatalogo.Domain.Responses;
using APICatalogo.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace APICatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        [Authorize(policy: "Admin")]
        [HttpPost]
        [Route("revoke/{username}")]
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

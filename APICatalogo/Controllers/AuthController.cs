using APICatalogo.Application.Commands.Identity;
using APICatalogo.Application.Queries.Categoria;
using APICatalogo.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
            var token = await _mediator.Send(command);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }

        [HttpPost]
        [Route("resgister")]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            var result = await _mediator.Send(command);
            if(result == false)
            {
                return BadRequest("Já há um usuário com esse username");
            }

            return Ok("Usuário criado com sucesso");
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenCommand command)
        {
            var token = await _mediator.Send(command);

            if (token == null)
            {
                return BadRequest();
            }

            return Ok(token);
        }

        [Authorize]
        [HttpPost]
        [Route("revoke/{username}")]
        public async Task<IActionResult> Revoke(string username)
        {
            var command = new RevokeCommand() { Username = username };
            var result = await _mediator.Send(command);
            if( result == false)
            {
                return BadRequest();
            }

            return Ok();
        }

    }
}

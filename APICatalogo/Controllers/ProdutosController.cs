using APICatalogo.Application.Queries.Produtos;
using APICatalogo.Domain.Queries.Produtos;
using APICatalogo.Infrastructure.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace APICatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {

        private readonly IMediator _mediator;

        public ProdutosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            var getMembersQuery = new GetProdutosQuery();
            var produtos = await _mediator.Send(getMembersQuery);

            if (produtos == null)
            {
                return NotFound("Não há produtos registrados.");
            }

            return Ok(produtos);

        }
    }
}

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
            var getProdutosQuery = new GetProdutosQuery();
            var produtos = await _mediator.Send(getProdutosQuery);

            if (produtos == null)
            {
                return NotFound("Não há produtos registrados.");
            }

            return Ok(produtos);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var getProdutoByIdQuery = new GetProdutoByIdQuery { Id = id };
            var produto = await _mediator.Send(getProdutoByIdQuery);

            if (produto == null)
            {
                return NotFound("Produto não encontrado.");
            }

            return Ok(produto);
        }
    }
}

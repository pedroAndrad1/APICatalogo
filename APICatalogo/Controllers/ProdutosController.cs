using APICatalogo.Application.Commands.Produto;
using APICatalogo.Application.Queries.Produtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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
        public async Task<IActionResult> Get([FromQuery] GetProdutosQuery getProdutosQuery)
        {
            var produtos = await _mediator.Send(getProdutosQuery);
            Response.Headers.Append("x-pagination-metadata", JsonSerializer.Serialize(produtos.Metadata));

            if (produtos == null)
            {
                return NotFound("Não há produtos registrados.");
            }

            return Ok(produtos.QueryResults);

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
        [HttpPost]
        public async Task<ActionResult> Add(AddProdutoCommand command)
        {
            var addedProduto = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = addedProduto.Id }, addedProduto);
        }
        [HttpPut]
        public async Task<ActionResult> Update(UpdateProdutoCommand command)
        {
            var updatedProduto = await _mediator.Send(command);

            return updatedProduto != null ? Ok(updatedProduto) : NotFound("Produto não encontrado");
        }
        [HttpDelete]
        public async Task<ActionResult> Delete(DeleteProdutoCommand command)
        {
            var deletedProduto = await _mediator.Send(command);
            return deletedProduto != null ? Ok(deletedProduto) : NotFound("Produto não encontrado");

        }

    }
}

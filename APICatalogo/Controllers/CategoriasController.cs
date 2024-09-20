using APICatalogo.Application.Commands.Categoria;
using APICatalogo.Application.Commands.Produto;
using APICatalogo.Application.Queries.Categoria;
using APICatalogo.Application.Queries.Produtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetCategoriaQuery getCategoriasQuery)
        {
            var categorias = await _mediator.Send(getCategoriasQuery);

            if (categorias == null)
            {
                return NotFound("Não há categorias registrados.");
            }

            return Ok(categorias);

        }
        [HttpGet("produtos")]
        public async Task<IActionResult> GetWithProdutos([FromQuery] GetCategoriaWithProdutosQuery getCategoriasWithProdutosQuery)
        {
            var categorias = await _mediator.Send(getCategoriasWithProdutosQuery);

            if (categorias == null)
            {
                return NotFound("Não há categorias registrados.");
            }

            return Ok(categorias);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var getCategoriaByIdQuery = new GetCatoriaByIdQuery { Id = id };
            var categoria = await _mediator.Send(getCategoriaByIdQuery);

            if (categoria == null)
            {
                return NotFound("Categoria não encontrada.");
            }

            return Ok(categoria);
        }
        [HttpPost]
        public async Task<ActionResult> Add(AddCategoriaCommand command)
        {
            var addedCategoria = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = addedCategoria.Id }, addedCategoria);
        }
        [HttpPut]
        public async Task<ActionResult> Update(UpdateCategoriaCommand command)
        {
            var updatedCategoria = await _mediator.Send(command);

            return updatedCategoria != null ? Ok(updatedCategoria) : NotFound("Categoria não encontrada");
        }
        [HttpDelete]
        public async Task<ActionResult> Delete(DeleteCategoriaCommand command)
        {
            var deletedProduto = await _mediator.Send(command);
            return deletedProduto != null ? Ok(deletedProduto) : NotFound("Categoria não encontrada");

        }
    }
}

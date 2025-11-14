using FilmesAPI.Models.DTOs;
using FilmesAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessaoController : ControllerBase
    {
        private readonly ISessaoService _service;

        public SessaoController(ISessaoService service)
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostSessao([FromQuery] CreateSessaoDto create)
        {
            await _service.Create(create);

            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllSessoes([FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            var listaSessoesPag = _service.GetAll(skip, take);

            if (listaSessoesPag == null)
            {
                return NotFound();
            }

            return Ok(listaSessoesPag);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSessaoById(int id)
        {
            var Sessao = await _service.GetById(id);

            if (Sessao == null)
            {
                return NotFound();
            }

            return Ok(Sessao);
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateSessao(int id, [FromQuery] UpdateSessaoDto update)
        {
            await _service.Update(id, update);

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSessao([FromRoute] int id)
        {
            await _service.Delete(id);

            return NoContent();
        }
    }
}
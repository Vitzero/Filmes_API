using FilmesAPI.Data;
using FilmesAPI.Models;
using FilmesAPI.Models.DTOs;
using FilmesAPI.Service;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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
        public async Task<IActionResult> CriarSessao([FromQuery] CreateSessaoDto create)
        {

            await _service.CriarSessao(create);

            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult PegarPaginado([FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            var listaSessoesPag = _service.PegarFilmesPag(skip,take);

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
        public async Task<IActionResult> PegarSessaoPorId(int id)
        {
            var Sessao = await _service.PegarSessaoPorId(id);

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
        public async Task<IActionResult> AtualizarSessao(int id, [FromQuery] UpdateSessaoDto update)
        {

            await _service.AtualizarSessao(id, update);


            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletandoPorId([FromRoute] int id)
        {
            await _service.DeleteSessao(id);

            return NoContent();
        }
    }
}

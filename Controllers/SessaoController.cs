using FilmesAPI.Data;
using FilmesAPI.Models;
using FilmesAPI.Models.DTOs.Sessao;
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
        private FilmeContext _context;

        private readonly ISessaoService _service;

        public SessaoController(FilmeContext Sessoes)
        {
            _context = Sessoes;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CriarSessao([FromQuery] CreateSessaoDto create)
        {

            _service.CriarSessao(create);

            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult PegarPaginado([FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            var listaSessoesPag = _service.PegarFilmesPag(skip,take);

            return Ok(listaSessoesPag);
        }

        [HttpGet]
        [Route("{filmeId}/{CinemaId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult PegarSessaoPorId(int id)
        {
            var Sessao = _service.PegarSessaoPorId(id);

            return Ok(Sessao);
        }

        [HttpPut]
        [Route("{filmeId}/{cinemaId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AtualizarSessao(int id, [FromQuery] UpdateSessaoDto update)
        {

            await _service.AtualizarSessao(id, update);


            return NoContent();
        }

        [HttpDelete]
        [Route("sessao/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletandoPorId([FromRoute] int id)
        {
            await _service.DeleteSessao(id);


            var sessaoToDelete = _context.Sessoes.FirstOrDefault(s => s.Id == id);
            if(sessaoToDelete == null)
            {
                return NotFound();
            }

            _context.Sessoes.Remove(sessaoToDelete);
            _context.SaveChanges();

            return NoContent();
        }
    }
}

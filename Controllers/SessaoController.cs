using FilmesAPI.Data;
using FilmesAPI.Data.DTOs.Sessao;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessaoController : ControllerBase
    {
        private FilmeContext _context;

        public SessaoController(FilmeContext Sessoes)
        {
            _context = Sessoes;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CriarSessao([FromQuery] CreateSessaoDto create)
        {
            if (create == null) {
                return BadRequest();
            }

            Sessao sessao = new Sessao()
            {
                FilmeId =  create.FilmeId,
                CinemaId = create.CinemaId
            };

            _context.Sessoes.Add(sessao);
            _context.SaveChanges();

            return Created();

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult PegarPaginado([FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            var listaSessoes = _context.Sessoes
                .OrderBy(x => x.CinemaId)
                .Skip(skip)
                .Take(take)
                .Select(c => new ReadSessaoDto()
                {
                    FilmeId = c.FilmeId,
                    CinemaId = c.CinemaId
                }
                )
                .ToList();

            return Ok(listaSessoes);
        }



        [HttpGet]
        [Route("{filmeId}/{CinemaId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult PegarSessaoPorId(int filmeId, int cinemaId)
        {
            var sessao = _context.Sessoes.FirstOrDefault(s => s.FilmeId == filmeId && s.CinemaId == cinemaId);
            if (sessao == null)
            {
                return NotFound();
            }

            ReadSessaoDto sessaoDto = new()
            {
                CinemaId = cinemaId,
                FilmeId = filmeId 
                
            };

            return Ok(sessaoDto);
        }

        [HttpPut]
        [Route("{filmeId}/{cinemaId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AtualizarSessao(int filmeid, int cinemaid, [FromQuery] UpdateSessaoDto update) {

            var SessaoToAtt = _context.Sessoes.FirstOrDefault(s => s.FilmeId == filmeid && s.CinemaId == cinemaid);

            if (SessaoToAtt == null)
            {
                return NotFound();
            }

            SessaoToAtt.FilmeId = update.FilmeId;
            SessaoToAtt.CinemaId = update.CinemaId;

            _context.SaveChanges();

            return Ok(SessaoToAtt);

        }

        [HttpDelete]
        [Route("{filmeId}/{cinemaId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeletandoPorId(int filmeId, int cinemaId)
        {
            var sessaoToDelete = _context.Sessoes.FirstOrDefault(s => s.FilmeId == filmeId && s.CinemaId == cinemaId);
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

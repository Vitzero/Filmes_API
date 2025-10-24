using FilmesAPI.Data;
using FilmesAPI.Data.DTOs;
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
                SessaoId = create.SessaoId
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
                .OrderBy(x => x.SessaoId)
                .Skip(skip)
                .Take(take)
                .Select(c => new ReadSessaoDto()
                {
                    SessaoId = c.SessaoId
                }
                )
                .ToList();

            return Ok(listaSessoes);
        }



        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult PegarSessaoPorId(int id)
        {
            var sessao = _context.Sessoes.FirstOrDefault(s => s.SessaoId == id);
            if (sessao == null)
            {
                return NotFound();
            }

            ReadSessaoDto sessaoDto = new()
            {
                SessaoId = sessao.SessaoId,
            };

            return Ok(sessaoDto);
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AtualizarSessao(int id, [FromQuery] UpdateSessaoDto update) {

            var SessaoToAtt = _context.Sessoes.FirstOrDefault(s => s.SessaoId == id);

            if (SessaoToAtt == null)
            {
                return NotFound();
            }

            SessaoToAtt.SessaoId = update.SessaoId;

            _context.SaveChanges();

            return Ok(SessaoToAtt);

        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeletandoPorId(int id)
        {
            var sessaoToDelete = _context.Sessoes.FirstOrDefault(s => s.SessaoId == id);
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

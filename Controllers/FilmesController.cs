using FilmesAPI.Data;
using FilmesAPI.Data.DTOs;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class FilmesController : ControllerBase
{
    private FilmeContext _context;
    public FilmesController(FilmeContext context)
    {
        _context = context;

    }

    [HttpPost]
    public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
    {
        Filme filme = new Filme
        {
            Titulo = filmeDto.Titulo,
            Duracao = filmeDto.Duracao,
            Genero = filmeDto.Genero,
        };

        _context.Filmes.Add(filme);
        _context.SaveChanges();
        return CreatedAtAction(nameof(PegarFilmesPorID), new { filme.Id }, filme);
    }

    [HttpGet]
    public IEnumerable<Filme> PegarFilmes([FromQuery]int skip = 0, [FromQuery] int take = 50)
    {
        return _context.Filmes.Skip(skip).Take(take);
    }


    [HttpGet("todos")]
    public List<Filme> PegarAllFilmes()
    {
        return _context.Filmes.ToList();
    }


    [HttpGet("{id}")]
    public IActionResult PegarFilmesPorID(int id)
    {
        var filme = 
            _context.Filmes
            .FirstOrDefault(f => f.Id == id);

        if (filme == null) return NotFound();
        return Ok(filme);
    }



}

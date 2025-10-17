using Azure;
using FilmesAPI.Data;
using FilmesAPI.Data.DTOs;
using FilmesAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
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
        Filme filme = new()
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
    public IActionResult PegarFilmes([FromQuery]int skip = 0, [FromQuery] int take = 50)
    {
        var listaFilmesDto = _context.Filmes
        .OrderBy(f => f.Id) // garante ordem consistente
        .Skip(skip)
        .Take(take)
        .Select(filme => new ReadFilmeDto
        {
            Titulo = filme.Titulo,
            Genero = filme.Genero,
            Duracao = filme.Duracao
        })
        .ToList();

        return Ok(listaFilmesDto);
    }


    [HttpGet("todos")]
    public IActionResult PegarAllFilmes()
    {
        List<ReadFilmeDto> ListaFilmes = [];

        foreach (var filme in _context.Filmes)
        {
            ListaFilmes.Add(new ReadFilmeDto
            {
                Titulo = filme.Titulo,
                Genero = filme.Genero,
                Duracao= filme.Duracao
            });
        }

        return Ok(ListaFilmes);
    }


    [HttpGet("{id}")]
    public IActionResult PegarFilmesPorID(int id)
    {
        var filme = 
            _context.Filmes
            .FirstOrDefault(f => f.Id == id);
        if (filme == null) return NotFound();

        ReadFilmeDto filmeDto = new()
        {
            Titulo = filme.Titulo,
            Genero = filme.Genero,
            Duracao = filme.Duracao
        };


        return Ok(filmeDto);
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaFilme(int id, [FromBody]UpdateFilmeDto update)
    {
        var filme = _context.Filmes
            .FirstOrDefault(f => f.Id == id);

        if (filme == null) return NotFound();

        filme.Titulo = update.Titulo;
        filme.Genero = update.Genero;
        filme.Duracao = update.Duracao;

        _context.SaveChanges();

        return NoContent();
    }
    [HttpPut]
    public IActionResult AtualizaFilmeParcialmente(int id, JsonPatchDocument<UpdateFilmeDto> patch)
    {
        var filme = _context.Filmes
            .FirstOrDefault(f => f.Id == id);

        if (filme == null) return NotFound();

        var filmeParaAtualizar = new UpdateFilmeDto
        {
            Titulo = filme.Titulo,
            Genero = filme.Genero,
            Duracao = filme.Duracao
        };

        patch.ApplyTo(filmeParaAtualizar, ModelState);

        if (!TryValidateModel(filmeParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }

        filme.Titulo = filmeParaAtualizar.Titulo;
        filme.Genero = filmeParaAtualizar.Genero;
        filme.Duracao = filmeParaAtualizar.Duracao;

        _context.SaveChanges();

        return NoContent();
    }


    [HttpDelete("{id}")]
    public IActionResult RemoverFilme(int id)
    {
        var filme = _context.Filmes
            .FirstOrDefault(f => f.Id == id);

        if (filme == null) return NotFound();

        _context.Filmes.Remove(filme);

        _context.SaveChanges();

        return NoContent();
    }

}

using Azure;
using FilmesAPI.Data;
using FilmesAPI.Models;
using FilmesAPI.Models.DTOs.Filme;
using FilmesAPI.Models.DTOs.Sessao;
using FilmesAPI.Service;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class FilmesController : ControllerBase
{
    private readonly IFilmesService _service;


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
    {
        
        _service.CriarFilme(filmeDto);

        return NoContent();
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult PegarFilmes([FromQuery] int skip = 0, [FromQuery] int take = 50)
    {
        var listaFilmesDto = _service.PegarFilmes(skip, take);

        if(listaFilmesDto == null)
        {
            return NoContent();
        }

        return Ok(listaFilmesDto);
    }


    [HttpGet("todos")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> PegarAllFilmes()
    {
        var filmes = _service.PegarFilmesSemPaginacao();

        if(filmes == null)
        {
            NoContent();
        }

        return Ok(filmes);
    }


    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult PegarFilmesPorID(int id)
    {
        var filmeDto = _service.PegarFilmePorID(id);

        return Ok(filmeDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult AtualizaFilme(int id, [FromBody]UpdateFilmeDto update)
    {
        _service.AtualizaFilme(update, id);


        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult RemoverFilme(int id)
    {
        _service.RemoverFilme(id);

        return NoContent();
    }

}

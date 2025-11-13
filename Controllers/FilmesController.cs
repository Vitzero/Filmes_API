using FilmesAPI.Models.DTOs;
using FilmesAPI.Service;
using Microsoft.AspNetCore.Mvc;


namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class FilmesController : ControllerBase
{
    private readonly IFilmesService _service;

    public FilmesController(IFilmesService _service)
    {
        this._service = _service;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
    {
        await _service.CriarFilme(filmeDto);

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
    public async Task<IActionResult> RemoverFilme(int id)
    {
        await _service.RemoverFilme(id);

        return NoContent();
    }

}

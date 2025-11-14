using FilmesAPI.Models.DTOs;
using FilmesAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmesController : ControllerBase
{
    private readonly IFilmesService _service;

    public FilmesController(IFilmesService service)
    {
        _service = service;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> PostFilme([FromBody] CreateFilmeDto filmeDto)
    {
        await _service.Create(filmeDto);

        return NoContent();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult GetFilmes([FromQuery] int skip = 0, [FromQuery] int take = 50)
    {
        var listaFilmesDto = _service.GetFilmes(skip, take);

        if (listaFilmesDto == null)
        {
            return NoContent();
        }

        return Ok(listaFilmesDto);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetFilmeById(int id)
    {
        var filmeDto = await _service.GetFilmeById(id);

        if (filmeDto == null)
        {
            return NotFound();
        }

        return Ok(filmeDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateFilme(int id, [FromBody] UpdateFilmeDto update)
    {
        await _service.UpdateFilme(update, id);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteFilme(int id)
    {
        await _service.DeleteFilme(id);

        return NoContent();
    }
}
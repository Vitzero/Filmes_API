using FilmesAPI.Models.DTOs;
using FilmesAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[Controller]")]
public class CinemaController : ControllerBase
{
    public CinemaController(ICinemaService cinemaService)
    {
        _cinemaService = cinemaService;
    }

    private readonly ICinemaService _cinemaService;

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostCinema([FromBody] CreateCinemaDto cinema)
    {
        await _cinemaService.Create(cinema);

        return NoContent();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCinemas([FromQuery] int skip = 0, [FromQuery] int take = 50)
    {
        var cinemasList = await _cinemaService.GetAll(skip, take);

        if (cinemasList == null)
        {
            return NotFound();
        }

        return Ok(cinemasList);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCinemaById(int id)
    {
        var cinema = await _cinemaService.GetById(id);

        if (cinema == null)
        {
            return NotFound();
        }

        return Ok(cinema);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCinema(int id, [FromBody] UpdateCinemaDto update)
    {
        await _cinemaService.Update(update, id);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteById(int id)
    {
        await _cinemaService.Delete(id);

        return NoContent();
    }
}
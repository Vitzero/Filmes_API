using FilmesAPI.Data;
using FilmesAPI.Models.DTOs;
using FilmesAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[Controller]")]

public class CinemaController : ControllerBase
{
  

    private readonly ICinemaService _cinemaService;

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult AdicionaCinema([FromBody] CreateCinemaDto cinema)
    {
        _cinemaService.CriarCinema(cinema);

        return NoContent();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Cinemas([FromQuery] int skip = 0, [FromQuery] int take = 50)
    {
        var cinemasList = _cinemaService.GetCinemas(skip, take);

        return Ok(cinemasList);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult CinemaPorId(int id)
    {

        var cinema = _cinemaService.GetCinemaPorId(id);

        return Ok(cinema);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult AtualizarTudo([FromBody] int id, [FromQuery] UpdateCinemaDto update)
    {
        _cinemaService.AtualizarCinema(update, id);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeletePorId(int id)
    {
        _cinemaService.DeleteCinema(id);
        
        return NoContent();
    
    }


}

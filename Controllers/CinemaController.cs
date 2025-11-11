using FilmesAPI.Data;
using FilmesAPI.Models;
using FilmesAPI.Models.DTOs.Cinema;
using FilmesAPI.Models.DTOs.Endereco;
using FilmesAPI.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[Controller]")]

public class CinemaController : ControllerBase
{
    private FilmeContext _context;

    private readonly ICinemaService _cinemaService;

    public CinemaController(FilmeContext context)
    {
        _context = context;
    }

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
        var cinema = _context.Cinemas
            .FirstOrDefault(b => b.Id == id);

        if (cinema == null)
        {
            return NotFound();
        }

        cinema.Nome = update.Nome;

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeletePorId(int id)
    {
        var cinema = _context.Cinemas.FirstOrDefault(b=>b.Id == id);
        if(cinema == null)
        {
            return NotFound();
        }
        
        _context.Cinemas.Remove(cinema);
        _context.SaveChanges();
        
        return NoContent();
    
    }


}

using FilmesAPI.Data;
using FilmesAPI.Data.DTOs;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[Controller]")]

public class CinemaController : ControllerBase
{
    private FilmeContext _context;

    public CinemaController(FilmeContext context)
    {
        _context = context;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult AdicionaCinema([FromBody] CreateCinemaDto cinema)
    {
        if (cinema == null)
        {
            return BadRequest();
        }

        Cinema cinemaModel = new()
        {
            Nome = cinema.Nome,
        };

        _context.Cinemas.Add(cinemaModel);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Cinemas), new { cinemaModel.Id }, cinemaModel);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Cinemas([FromQuery] int skip = 0, [FromQuery] int take = 50)
    {
        var cinemasList = _context.Cinemas
            .OrderBy(f=>f.Id)
            .Skip(skip)
            .Take(take)
            .ToList();

        if(cinemasList.Count == 0)
        {
            return NotFound();
        }

        return Ok(cinemasList);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult CinemaPorId([FromBody]int id)
    {
        var cinema = _context.Cinemas
            .OrderBy(f => f.Id)
            .FirstOrDefault(f => f.Id == id);
        
        if (cinema == null)
        {
            return NotFound();
        }
        ReadCinemaDto response = new()
        {
            Nome = cinema.Nome
        };
       
        return Ok(response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult AtualizarTudo([FromBody] int id,[FromQuery] UpdateCinemaDto update)
    {
        var cinema = _context.Cinemas
            .FirstOrDefault(b=>b.Id == id);

        if (cinema == null)
        {
            return NotFound();
        }
        
        cinema.Nome = update.Nome;

        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeletePorId([FromBody]int id)
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

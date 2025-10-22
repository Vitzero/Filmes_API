using FilmesAPI.Data;
using FilmesAPI.Data.DTOs;
using FilmesAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class EnderecoController : ControllerBase
{
    private FilmeContext _context;

    public EnderecoController(FilmeContext context)
    {
        _context = context;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AdicionaEndereco([FromBody] CreateEnderecoDto Endereco)
    {
        Endereco endereco = new()
        {
            Numero = Endereco.Numero,
            Logradouro = Endereco.Logradouro,
        };

        _context.Enderecos.Add(endereco);
        _context.SaveChanges();
        return CreatedAtAction(nameof(PegarEnderecoPorID), new { id = endereco.Id }, endereco);
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult PegarEndereco([FromQuery] int skip = 0, [FromQuery] int take = 50)
    {
        var listaEnderecoDto = _context.Enderecos
        .OrderBy(f => f.Id) // garante ordem consistente
        .Skip(skip)
        .Take(take)
        .Select(endereco => new ReadEnderecoDto
        {
            Logradouro = endereco.Logradouro,
            Numero = endereco.Numero,
            Id = endereco.Id
        })
        .ToList();

        return Ok(listaEnderecoDto);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult PegarEnderecoPorID(int id)
    {
        var endereco =
            _context.Enderecos
            .FirstOrDefault(f => f.Id == id);
        if (endereco == null) return NotFound();

        ReadEnderecoDto enderecoDto = new()
        {
            Logradouro = endereco.Logradouro,
            Numero = endereco.Numero
        };


        return Ok(enderecoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult AtualizarEndereco(int id, [FromBody] UpdateEnderecoDto update)
    {
        var endereco = _context.Enderecos
            .FirstOrDefault(f => f.Id == id);

        if (endereco == null) return NotFound();

        endereco.Logradouro = update.Logradouro;
        endereco.Numero = update.Numero;

        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult RemoverEndereco(int id)
    {
        var endereco = _context.Enderecos
            .FirstOrDefault(f => f.Id == id);

        if (endereco == null)
        {
            return NotFound();
        }

        _context.Enderecos.Remove(endereco);

        _context.SaveChanges();

        return NoContent();
    }
}

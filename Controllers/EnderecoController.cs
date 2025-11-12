using FilmesAPI.Data;
using FilmesAPI.Models;
using FilmesAPI.Models.DTOs;
using FilmesAPI.Repository;
using FilmesAPI.Service;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class EnderecoController : ControllerBase
{
    private readonly IEnderecoService _enderecoService;

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AdicionaEndereco([FromBody] CreateEnderecoDto Endereco)
    {
        _enderecoService.CreateEndereco(Endereco);

        return NoContent();
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult PegarEndereco([FromQuery] int skip = 0, [FromQuery] int take = 50)
    {
        var enderecos = _enderecoService.GetEnderecos(skip, take);

        return Ok(enderecos);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult PegarEnderecoPorID(int id)
    {
        var endereco = _enderecoService.GetEnderecoById(id);

        return Ok(endereco);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult AtualizarEndereco(int id, [FromBody] UpdateEnderecoDto update)
    {
        _enderecoService.UpdateEndereco(update, id);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult RemoverEndereco(int id)
    {
        _enderecoService.DeleteEndereco(id);

        return NoContent();
    }
}

using FilmesAPI.Models.DTOs;
using FilmesAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class EnderecoController : ControllerBase
{
    private readonly IEnderecoService _enderecoService;

    public EnderecoController(IEnderecoService enderecoService)
    {
        _enderecoService = enderecoService;
    }

    [HttpPost]
    public async Task<IActionResult> PostEndereco([FromBody] CreateEnderecoDto endereco)
    {
        await _enderecoService.Create(endereco);
        return NoContent();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetEndereco([FromQuery] int skip = 0, [FromQuery] int take = 50)
    {
        var enderecos = _enderecoService.GetAll(skip, take);

        if (enderecos == null)
        {
            return NotFound();
        }

        return Ok(enderecos);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetEnderecoById(int id)
    {
        var endereco = _enderecoService.GetById(id);

        if (endereco == null)
        {
            return NotFound();
        }

        return Ok(endereco);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateEndereco(int id, [FromBody] UpdateEnderecoDto update)
    {
        await _enderecoService.Update(update, id);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteEndereco(int id)
    {
        await _enderecoService.Delete(id);

        return NoContent();
    }
}
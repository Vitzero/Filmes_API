using Azure;
using FilmesAPI.Data;
using FilmesAPI.Data.DTOs;
using FilmesAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class FilmesController : ControllerBase
{
    private FilmeContext _context;
    public FilmesController(FilmeContext context)
    {
        _context = context;

    }

    /// <summary>
    /// Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="filmeDto">Objeto com os campos necessários para criação de um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
    {
        Filme filme = new()
        {
            Titulo = filmeDto.Titulo,
            Duracao = filmeDto.Duracao,
            Genero = filmeDto.Genero,
        };

        _context.Filmes.Add(filme);
        _context.SaveChanges();
        return CreatedAtAction(nameof(PegarFilmesPorID), new { filme.Id }, filme);
    }

    /// <summary>
    /// Retorna uma lista paginada de filmes.
    /// </summary>
    /// <param name="skip">Número de filmes a serem ignorados.</param>
    /// <param name="take">Número de filmes a serem retornados.</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Retorna a lista de filmes.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult PegarFilmes([FromQuery]int skip = 0, [FromQuery] int take = 50)
    {
        var listaFilmesDto = _context.Filmes
        .OrderBy(f => f.Id) // garante ordem consistente
        .Skip(skip)
        .Take(take)
        .Select(filme => new ReadFilmeDto
        {
            Titulo = filme.Titulo,
            Genero = filme.Genero,
            Duracao = filme.Duracao
        })
        .ToList();

        return Ok(listaFilmesDto);
    }

    /// <summary>
    /// Retorna todos os filmes sem paginação.
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <response code="200">Retorna todos os filmes cadastrados.</response>
    [HttpGet("todos")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult PegarAllFilmes()
    {
        List<ReadFilmeDto> ListaFilmes = [];

        foreach (var filme in _context.Filmes)
        {
            ListaFilmes.Add(new ReadFilmeDto
            {
                Titulo = filme.Titulo,
                Genero = filme.Genero,
                Duracao= filme.Duracao
            });
        }

        return Ok(ListaFilmes);
    }

    /// <summary>
    /// Retorna um filme específico pelo ID.
    /// </summary>
    /// <param name="id">ID do filme a ser consultado.</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Retorna os dados do filme.</response>
    /// <response code="404">Caso o filme não seja encontrado.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult PegarFilmesPorID(int id)
    {
        var filme = 
            _context.Filmes
            .FirstOrDefault(f => f.Id == id);
        if (filme == null) return NotFound();

        ReadFilmeDto filmeDto = new()
        {
            Titulo = filme.Titulo,
            Genero = filme.Genero,
            Duracao = filme.Duracao
        };


        return Ok(filmeDto);
    }

    /// <summary>
    /// Atualiza completamente os dados de um filme.
    /// </summary>
    /// <param name="id">ID do filme a ser atualizado.</param>
    /// <param name="update">Objeto com os novos dados do filme.</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Atualização feita com sucesso.</response>
    /// <response code="404">Caso o filme não seja encontrado.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult AtualizaFilme(int id, [FromBody]UpdateFilmeDto update)
    {
        var filme = _context.Filmes
            .FirstOrDefault(f => f.Id == id);

        if (filme == null) return NotFound();

        filme.Titulo = update.Titulo;
        filme.Genero = update.Genero;
        filme.Duracao = update.Duracao;

        _context.SaveChanges();

        return NoContent();
    }

    /// <summary>
    /// Atualiza parcialmente os dados de um filme.
    /// </summary>
    /// <param name="id">ID do filme a ser atualizado.</param>
    /// <param name="patch">Objeto JSON Patch com as operações de atualização.</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Atualização feita com sucesso.</response>
    /// <response code="400">Erro de validação nos dados enviados.</response>
    /// <response code="404">Caso o filme não seja encontrado.</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult AtualizaFilmeParcialmente(int id, JsonPatchDocument<UpdateFilmeDto> patch)
    {
        var filme = _context.Filmes
            .FirstOrDefault(f => f.Id == id);

        if (filme == null) return NotFound();

        var filmeParaAtualizar = new UpdateFilmeDto
        {
            Titulo = filme.Titulo,
            Genero = filme.Genero,
            Duracao = filme.Duracao
        };

        patch.ApplyTo(filmeParaAtualizar, ModelState);

        if (!TryValidateModel(filmeParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }

        filme.Titulo = filmeParaAtualizar.Titulo;
        filme.Genero = filmeParaAtualizar.Genero;
        filme.Duracao = filmeParaAtualizar.Duracao;

        _context.SaveChanges();

        return NoContent();
    }

    /// <summary>
    /// Remove um filme do banco de dados.
    /// </summary>
    /// <param name="id">ID do filme a ser removido.</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Remoção feita com sucesso.</response>
    /// <response code="404">Caso o filme não seja encontrado.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult RemoverFilme(int id)
    {
        var filme = _context.Filmes
            .FirstOrDefault(f => f.Id == id);

        if (filme == null) 
        {     
            return NotFound(); 
        }

        _context.Filmes.Remove(filme);

        _context.SaveChanges();

        return NoContent();
    }

}

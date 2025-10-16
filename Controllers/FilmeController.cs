using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class FilmeController : ControllerBase
{
    private static List<Filme> Filmes = new List<Filme>();
    private static int id = 0;

    [HttpPost]
    public void AdicionaFilme([FromBody] Filme filme)
    {
        filme.Id = ++id;
        Filmes.Add(filme);
        Console.WriteLine($"{filme.Titulo} : {filme.Duracao}");
    }

    [HttpGet]
    public IEnumerable<Filme> PegarFilmes([FromQuery]int skip = 0, [FromQuery] int take = 50)
    {
        return Filmes.Skip(skip).Take(take);
    }


    [HttpGet]
    public List<Filme> PegarAllFilmes()
    {
        return Filmes;
    }

    [HttpGet("{id}")]
    public Filme? PegarFilmesPorID(int id)
    {
        return Filmes.FirstOrDefault(f => f.Id == id);
    }



}

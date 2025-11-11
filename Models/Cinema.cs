using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models;

public class Cinema
{

    public int Id { get; set; }
    public required string Nome { get; set; }
    public int EnderecoId { get; set; }
    public Endereco Endereco { get; set; }

}
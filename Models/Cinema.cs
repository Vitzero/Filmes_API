using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models;

public class Cinema
{
    public int Id { get; set; }

    public required string Nome { get; set; }


}
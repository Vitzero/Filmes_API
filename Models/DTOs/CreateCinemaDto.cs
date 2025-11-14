using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models.DTOs;

public class CreateCinemaDto
{
    [Required(ErrorMessage = "O campo 'Nome' é obrigatório.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O campo 'CinemaId' é obrigatório.")]
    public int CinemaId { get; set; }

    public Cinema ToEntity()
    {
        return new Cinema
        {
            Nome = Nome,
            EnderecoId = CinemaId
        };
    }
}
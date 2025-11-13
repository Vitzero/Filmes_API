using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models.DTOs;

public class CreateCinemaDto
{
    [Required(ErrorMessage = "O campo de nome é obrigatório.")]
    public string Nome { get; set; }
    [Required(ErrorMessage = "O campo de CinemaId é obrigatório.")]
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

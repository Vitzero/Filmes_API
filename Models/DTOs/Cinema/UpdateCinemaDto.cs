using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models.DTOs.Cinema
{
    public class UpdateCinemaDto
    {
        [Required(ErrorMessage = "O campo NOME é obrigatório!")]
        public string Nome { get; set; }
    }
}

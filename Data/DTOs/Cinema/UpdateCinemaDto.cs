using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.DTOs.Cinema
{
    public class UpdateCinemaDto
    {
        [Required(ErrorMessage = "O campo NOME é obrigatório!")]
        public string Nome { get; set; }
    }
}

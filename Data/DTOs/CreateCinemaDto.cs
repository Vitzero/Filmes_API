using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.DTOs
{
    public class CreateCinemaDto
    {
        [Required(ErrorMessage = "O campo NOME é obrigatório!")]
        public string Nome { get; set; }
    }
}

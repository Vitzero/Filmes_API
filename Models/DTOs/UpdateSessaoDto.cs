using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models.DTOs
{
    public class UpdateSessaoDto
    {
        [Required(ErrorMessage = "O campo FilmeId é obrigatório!")]
        public int FilmeId { get; set; }

        [Required(ErrorMessage = "O campo CinemaId é obrigatório!")]
        public int CinemaId { get; set; }
    }
}

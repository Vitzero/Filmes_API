using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models.DTOs
{
    public class CreateSessaoDto
    {
        [Required(ErrorMessage = "O FilmeId é obrigatório!")]
        public int FilmeId { get; set; }
        [Required(ErrorMessage = "O CinemaId é obrigatório!")]
        public int CinemaId { get; set; }
    }
}

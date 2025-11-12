using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models.DTOs
{
    public class CreateSessaoDto
    {
        [Required]
        public int FilmeId { get; set; }
        public int CinemaId { get; set; }
    }
}

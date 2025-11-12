using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models.DTOs
{
    public class UpdateSessaoDto
    {
        [Required]
        public int FilmeId { get; set; }

        [Required]
        public int CinemaId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.DTOs
{
    public class UpdateSessaoDto
    {
        [Required]
        public int SessaoId { get; set; }
    }
}

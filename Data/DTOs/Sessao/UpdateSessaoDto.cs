using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.DTOs.Sessao
{
    public class UpdateSessaoDto
    {
        [Required]
        public int SessaoId { get; set; }
    }
}

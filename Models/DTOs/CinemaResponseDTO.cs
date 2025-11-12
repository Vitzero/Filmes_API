using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models.DTOs
{
    public class CinemaResponseDTO
    {
        public DateTime HoraDaConsulta { get; set; } = DateTime.Now;
        public string Nome { get; set; }
        public EnderecoResponseDTO ReadEnderecoDto { get; set; }
        public List<ReadSessaoDto> Sessoes { get; set; }
    }
}

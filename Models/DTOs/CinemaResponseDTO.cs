namespace FilmesAPI.Models.DTOs
{
    public class CinemaResponseDTO
    {
        public DateTime HoraDaConsulta { get; set; } = DateTime.Now;
        public string Nome { get; set; }
        public int EnderecoId { get; set; }
        public List<SessaoResponseDTO> Sessoes { get; set; }
    }
}
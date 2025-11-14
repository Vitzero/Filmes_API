namespace FilmesAPI.Models.DTOs;

public class FilmeResponseDTO
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Genero { get; set; }
    public int Duracao { get; set; }
    public DateTime HoraDaConsulta { get; set; } = DateTime.Now;
    public ICollection<SessaoResponseDTO> Sessoes { get; set; }
}
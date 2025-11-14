using FilmesAPI.Models.DTOs;

namespace FilmesAPI.Models;

public class Cinema
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int EnderecoId { get; set; }
    public virtual Endereco Endereco { get; set; }

    public CinemaResponseDTO ToDto()
    {
        return new CinemaResponseDTO
        {
            Nome = Nome,
            EnderecoId = EnderecoId,
            HoraDaConsulta = DateTime.Now,
        };
    }
}
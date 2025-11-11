using FilmesAPI.Models.DTOs.Endereco;
using FilmesAPI.Models.DTOs.Sessao;
using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models.DTOs.Cinema
{
    public class ReadCinemaDto
    {
        public DateTime HoraDaConsulta { get; set; } = DateTime.Now;
        public int Id { get; set; }
        public string Nome { get; set; }
        public ReadEnderecoDto ReadEnderecoDto { get; set; }
        public List<ReadSessaoDto> Sessoes { get; set; }
    }
}

using FilmesAPI.Data.DTOs.Endereco;
using FilmesAPI.Data.DTOs.Sessao;
using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.DTOs.Cinema
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

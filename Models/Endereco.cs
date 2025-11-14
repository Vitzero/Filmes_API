using FilmesAPI.Models.DTOs;

namespace FilmesAPI.Models
{
    public class Endereco
    {
        public int Id { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }

        public EnderecoResponseDTO ToDto()
        {
            return new EnderecoResponseDTO
            {
                Logradouro = Logradouro,
                Numero = Numero,
            };
        }
    }
}
using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models
{
    public class Endereco
    {
        public int Id { get; set; }
        public required string Logradouro { get; set; }
        public required int Numero { get; set; }

    }
}

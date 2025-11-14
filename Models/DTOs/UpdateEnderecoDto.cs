using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models.DTOs
{
    public class UpdateEnderecoDto
    {
        [Required(ErrorMessage = "O campo Logradouro é obrigatório!")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "O campo Numero é obrigatório!")]
        public int Numero { get; set; }
    }
}
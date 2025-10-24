using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.DTOs.Endereco
{
    public class CreateEnderecoDto
    {
        [Required(ErrorMessage = "O campo 'Logradouro' é obrigatório!")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "O campo 'Numero' é obrigatório!")]
        public int Numero { get; set; }
    }
}

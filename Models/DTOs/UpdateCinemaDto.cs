using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models.DTOs
{
    public class UpdateCinemaDto
    {
        [Required(ErrorMessage = "O campo NOME é obrigatório!")]
        public string Nome { get; set; }

        public int EnderecoId { get; set; }


        public Cinema ToEntity()
        {
            return new Cinema
            {
                Nome = Nome,
                EnderecoId = EnderecoId
            };
        }

    }
}

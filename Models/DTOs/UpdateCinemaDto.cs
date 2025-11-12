using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models.DTOs
{
    public class UpdateCinemaDto
    {
        [Required(ErrorMessage = "O campo NOME é obrigatório!")]
        public string Nome { get; set; }

        public Endereco Endereco { get; set; }


        public Cinema ToEntity()
        {
            return new Cinema
            {
                Nome = Nome,
                Endereco = new Endereco
                {
                    Logradouro = Endereco.Logradouro,
                    Numero = Endereco.Numero
                }
            };
        }

    }
}

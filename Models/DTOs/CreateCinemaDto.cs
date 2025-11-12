using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models.DTOs;

public class CreateCinemaDto
{
    [Required(ErrorMessage = "O campo de nome é obrigatório.")]
    public string Nome { get; set; }
    public CreateEnderecoDto Endereco { get; set; }
    
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

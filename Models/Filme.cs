using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models
{
    public class Filme
    {
        [Required(ErrorMessage = "O título do filme é obrigatório!")] 
        public string Titulo { get; set; }
        
        [Required]
        [Range(70,600, ErrorMessage = "A duração do filme no minimo tem que ter 70 e no máximo 600!")]
        public int Duracao { get; set; }

        [Required(ErrorMessage = "O título do Genero é obrigatório!")] 
        [MaxLength(50, ErrorMessage = "O tamanho do genero não pode exeder 50 caracteres")]
        public string Genero { get; set; }

    }
}

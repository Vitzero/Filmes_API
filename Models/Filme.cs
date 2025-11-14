using FilmesAPI.Models.DTOs;

namespace FilmesAPI.Models
{
    public class Filme
    {
        public int Id { get; set; }

        public string Titulo { get; set; }

        public int Duracao { get; set; }

        public string Genero { get; set; }

        public virtual ICollection<Sessao> Sessoes { get; set; }

        public FilmeResponseDTO ToDto()
        {
            return new FilmeResponseDTO
            {
                Titulo = Titulo,
                Duracao = Duracao,
                Genero = Genero
            };
        }
    }
}
using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models
{
    public class Filme
    {

        public int Id { get; set; }

        public required string Titulo { get; set; }
        
        public required int Duracao { get; set; }

        public required string Genero { get; set; }

        public virtual ICollection<Sessao> Sessoes { get; set; }

    }
}

namespace FilmesAPI.Data.DTOs.Sessao
{
    public class ReadSessaoDto
    {
        public int SessaoId { get; set; }

        public int? CinemaId { get; set; }

        public int FilmeId { get; set; }
    } 
}

using FilmesAPI.Models.DTOs;
using FilmesAPI.Repository;

namespace FilmesAPI.Service
{
    public interface IFilmesService
    {
        Task Create(CreateFilmeDto filmeDto);

        Task<IList<FilmeResponseDTO>> GetFilmes(int skip, int take);

        Task<FilmeResponseDTO> GetFilmeById(int id);

        Task UpdateFilme(UpdateFilmeDto update, int id);

        Task DeleteFilme(int id);
    }

    public class FilmesService : IFilmesService
    {
        private readonly IFilmesRepository _filmesRepository;

        public FilmesService(IFilmesRepository filmesRepository)
        {
            _filmesRepository = filmesRepository;
        }

        public async Task Create(CreateFilmeDto filmeDto)
        {
            var filme = filmeDto.ToEntity();

            await _filmesRepository.Create(filme);
        }

        public async Task<IList<FilmeResponseDTO>> GetFilmes(int skip, int take)
        {
            var horaConsulta = DateTime.Now;

            var filmes = await _filmesRepository.GetAll(skip, take);

            var listaFilmesDto = filmes.Select(filme => new FilmeResponseDTO
            {
                Id = filme.Id,
                Titulo = filme.Titulo,
                Genero = filme.Genero,
                Duracao = filme.Duracao,
                HoraDaConsulta = horaConsulta,
                Sessoes = filme.Sessoes.Select(sessao => new SessaoResponseDTO
                {
                    CinemaId = sessao.CinemaId,
                    FilmeId = sessao.FilmeId
                }).ToList()
            }).ToList();

            return listaFilmesDto;
        }

        public async Task<FilmeResponseDTO> GetFilmeById(int id)
        {
            var filme = await _filmesRepository.GetById(id);

            FilmeResponseDTO filmeDto = filme.ToDto();

            return filmeDto;
        }

        public async Task UpdateFilme(UpdateFilmeDto update, int id)
        {
            var filme = await _filmesRepository.GetById(id);

            filme.Titulo = update.Titulo;
            filme.Genero = update.Genero;
            filme.Duracao = update.Duracao;

            await _filmesRepository.Update(filme);
        }

        public async Task DeleteFilme(int id)
        {
            var filme = await _filmesRepository.GetById(id);

            await _filmesRepository.Delete(filme);
        }
    }
}
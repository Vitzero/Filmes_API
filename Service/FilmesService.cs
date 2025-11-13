using FilmesAPI.Models;
using FilmesAPI.Models.DTOs;
using FilmesAPI.Repository;

namespace FilmesAPI.Service
{
    public interface IFilmesService
    {
        Task CriarFilme(CreateFilmeDto filmeDto);
        Task<List<ReadFilmeDto>> PegarFilmes(int skip, int take);
        Task<ReadFilmeDto> PegarFilmePorID(int id);
        Task AtualizaFilme(UpdateFilmeDto update, int id);
        Task RemoverFilme(int id);

    }

    public class FilmesService : IFilmesService
    {
        private readonly IFilmesRepository _filmesRepository;

        public FilmesService(IFilmesRepository _filmesRepository)
        {
            this._filmesRepository = _filmesRepository;
        }

        public async Task CriarFilme(CreateFilmeDto filmeDto)
        {
            Filme filme = new()
            {
                Titulo = filmeDto.Titulo,
                Duracao = filmeDto.Duracao,
                Genero = filmeDto.Genero,
            };

            await _filmesRepository.AddFilme(filme);

        }

        public async Task<List<ReadFilmeDto>> PegarFilmes(int skip, int take)
        {
            var horaConsulta = DateTime.Now;

            var filmes = await _filmesRepository.PegarFilmes(skip, take);


            var listaFilmesDto = filmes.Select(filme => new ReadFilmeDto
            {
                Id = filme.Id,
                Titulo = filme.Titulo,
                Genero = filme.Genero,
                Duracao = filme.Duracao,
                HoraDaConsulta = horaConsulta,
                Sessoes = filme.Sessoes.Select(sessao => new ReadSessaoDto
                {
                    CinemaId = sessao.CinemaId,
                    FilmeId = sessao.FilmeId
                }).ToList()
            }).ToList();


            return listaFilmesDto;
        }

        public async Task<ReadFilmeDto> PegarFilmePorID(int id)
        {

            var filme = await _filmesRepository.ObterPorIdAsync(id);
            

            ReadFilmeDto filmeDto = new()
            {
                Id = filme.Id,
                Titulo = filme.Titulo,
                Genero = filme.Genero,
                Duracao = filme.Duracao
            };

            return filmeDto;

        }

        public async Task AtualizaFilme(UpdateFilmeDto update, int id)
        {

            var filme = await _filmesRepository.ObterPorIdAsync(id);

            filme.Titulo = update.Titulo;
            filme.Genero = update.Genero;
            filme.Duracao = update.Duracao;

            await _filmesRepository.UpdateFilme(filme);

        }

        public async Task RemoverFilme(int id)
        {
            var filme = await _filmesRepository.ObterPorIdAsync(id);

           await _filmesRepository.RemoveFilme(filme);

        }
       

    }
}

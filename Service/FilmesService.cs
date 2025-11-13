using FilmesAPI.Models;
using FilmesAPI.Models.DTOs;
using FilmesAPI.Repository;

namespace FilmesAPI.Service
{
    public interface IFilmesService
    {
        Task CriarFilme(CreateFilmeDto filmeDto);
        Task PegarFilmes(int skip, int take);
        Task PegarFilmesSemPaginacao();
        ReadFilmeDto PegarFilmePorID(int id);
        Task AtualizaFilme(UpdateFilmeDto update, int id);
        Task RemoverFilme(int id);

    }

    public class FilmesService : IFilmesService
    {
        private readonly IFilmesRepository _filmesRepository;
        
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

        public async Task PegarFilmes(int skip, int take)
        {
            var horaConsulta = DateTime.Now;

            var listaFilmesDto = _filmesRepository.PegarFilmes()
                .OrderBy(f => f.Id)
                .Skip(skip)
                .Take(take)
                .Select(filme => new ReadFilmeDto
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
                })
                .ToList();
        }

        public async Task PegarFilmesSemPaginacao()
        {
            var horaConsulta = DateTime.Now;

            var listaFilmesDto = _filmesRepository.PegarFilmes()
                .OrderBy(f => f.Id)
                .Select(filme => new ReadFilmeDto
                {
                    Id = filme.Id,
                    Titulo = filme.Titulo,
                    Genero = filme.Genero,
                    Duracao = filme.Duracao,
                    HoraDaConsulta = horaConsulta,
                    Sessoes = filme.Sessoes.Select(sessao => new ReadSessaoDto
                    {
                        CinemaId = sessao.CinemaId,  // FK direta, não navegação
                        FilmeId = sessao.FilmeId      // FK direta, não navegação

                    }).ToList()
                })
                .ToList();
        }
        public ReadFilmeDto PegarFilmePorID(int id)
        {

            var filme =
            _filmesRepository.PegarFilmes()
            .FirstOrDefault(x => x.Id == id);

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

            var filme = _filmesRepository.PegarFilmes()
            .FirstOrDefault(f => f.Id == id);

            filme.Titulo = update.Titulo;
            filme.Genero = update.Genero;
            filme.Duracao = update.Duracao;

            await _filmesRepository.UpdateFilme(filme);

        }

        public async Task RemoverFilme(int id)
        {
            var filme = _filmesRepository.PegarFilmes()
           .FirstOrDefault(f => f.Id == id);

           _filmesRepository.RemoveFilme(filme);

        }
       

    }
}

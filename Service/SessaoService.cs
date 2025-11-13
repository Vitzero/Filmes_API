using FilmesAPI.Models;
using FilmesAPI.Models.DTOs;
using FilmesAPI.Repository;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Service
{
    public interface ISessaoService
    {
        Task CriarSessao(CreateSessaoDto create);
        List<ReadSessaoDto> PegarFilmesPag(int skip, int take);
        Task<ReadSessaoDto> PegarSessaoPorId(int id);
        Task AtualizarSessao(int id, UpdateSessaoDto update);
        Task DeleteSessao(int id);
    }
    public class SessaoService : ISessaoService
    {
        private readonly ISessaoRepository _repository;

        public SessaoService(ISessaoRepository _repository)
        {
            this._repository = _repository;
        }

        public async Task CriarSessao(CreateSessaoDto create)
        {

            Sessao sessao = new Sessao()
            {
                FilmeId = create.FilmeId,
                CinemaId = create.CinemaId
            };

            await _repository.CriarSessao(sessao);
        }

        public List<ReadSessaoDto> PegarFilmesPag(int skip, int take)
        {

            var listaSessoes = _repository.PegarSessoesPag( skip,  take)
                .OrderBy(x => x.CinemaId)
                .Skip(skip)
                .Take(take)
                .Select(c => new ReadSessaoDto()
                {
                    FilmeId = c.FilmeId,
                    CinemaId = c.CinemaId
                }
                )
                .ToList();

            return listaSessoes;
        }

        public async Task<ReadSessaoDto> PegarSessaoPorId(int id)
        {
            var sessao = await _repository.PegarSessaoPorId(id);
            
            if (sessao == null)
            {
                return null;
            }

            ReadSessaoDto sessaoDto = new()
            {
                CinemaId = sessao.CinemaId,
                FilmeId = sessao.FilmeId
            };

            return sessaoDto;
        }

        public async Task AtualizarSessao(int id, UpdateSessaoDto update)
        {
            var SessaoToAtt = await _repository.PegarSessaoPorId(id);

            SessaoToAtt.FilmeId = update.FilmeId;
            SessaoToAtt.CinemaId = update.CinemaId;

            await _repository.UpdateSessao(SessaoToAtt);
        }

        public async Task DeleteSessao(int id)
        {
            var sessao2Delete = await _repository.PegarSessaoPorId(id);

            await _repository.DeleteSessao(sessao2Delete);

        }

    }
}

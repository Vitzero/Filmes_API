using FilmesAPI.Models;
using FilmesAPI.Models.DTOs;
using FilmesAPI.Repository;

namespace FilmesAPI.Service
{
    public interface ISessaoService
    {
        Task Create(CreateSessaoDto create);

        IList<SessaoResponseDTO> GetAll(int skip, int take);

        Task<SessaoResponseDTO> GetById(int id);

        Task Update(int id, UpdateSessaoDto update);

        Task Delete(int id);
    }

    public class SessaoService : ISessaoService
    {
        private readonly ISessaoRepository _repository;

        public SessaoService(ISessaoRepository _repository)
        {
            this._repository = _repository;
        }

        public async Task Create(CreateSessaoDto create)
        {
            Sessao sessao = new Sessao()
            {
                FilmeId = create.FilmeId,
                CinemaId = create.CinemaId
            };

            await _repository.Create(sessao);
        }

        public IList<SessaoResponseDTO> GetAll(int skip, int take)
        {
            var listaSessoes = _repository.GetAll(skip, take)
                .OrderBy(x => x.CinemaId)
                .Skip(skip)
                .Take(take)
                .Select(c => new SessaoResponseDTO()
                {
                    FilmeId = c.FilmeId,
                    CinemaId = c.CinemaId
                }
                )
                .ToList();

            return listaSessoes;
        }

        public async Task<SessaoResponseDTO> GetById(int id)
        {
            var sessao = await _repository.GetById(id);

            if (sessao == null)
            {
                return null;
            }

            SessaoResponseDTO sessaoDto = new()
            {
                CinemaId = sessao.CinemaId,
                FilmeId = sessao.FilmeId
            };

            return sessaoDto;
        }

        public async Task Update(int id, UpdateSessaoDto update)
        {
            var SessaoToAtt = await _repository.GetById(id);

            SessaoToAtt.FilmeId = update.FilmeId;
            SessaoToAtt.CinemaId = update.CinemaId;

            await _repository.Update(SessaoToAtt);
        }

        public async Task Delete(int id)
        {
            var sessao2Delete = await _repository.GetById(id);

            await _repository.Delete(sessao2Delete);
        }
    }
}
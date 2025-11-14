using FilmesAPI.Data;
using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Repository
{
    public interface ISessaoRepository
    {
        Task Create(Sessao sessao);

        List<Sessao> GetAll(int skip, int take);

        Task<Sessao?> GetById(int id);

        Task Update(Sessao sessao);

        Task Delete(Sessao sessao);
    }

    public class SessaoRepository : ISessaoRepository
    {
        private readonly FilmeContext _dbContext;

        public SessaoRepository(FilmeContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        public async Task Create(Sessao sessao)
        {
            _dbContext.Sessoes.Add(sessao);
            await _dbContext.SaveChangesAsync();
        }

        public List<Sessao> GetAll(int skip, int take)
        {
            var listaPaginada = _dbContext.Sessoes.Skip(skip).Take(take).ToList();

            return listaPaginada;
        }

        public async Task<Sessao?> GetById(int id)
        {
            var sessao = await _dbContext.Sessoes.FirstOrDefaultAsync(s => s.Id == id);

            return sessao;
        }

        public async Task Update(Sessao sessao)
        {
            _dbContext.Sessoes.Update(sessao);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Sessao sessao)
        {
            _dbContext.Sessoes.Remove(sessao);
            await _dbContext.SaveChangesAsync();
        }
    }
}
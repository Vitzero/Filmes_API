using FilmesAPI.Data;
using FilmesAPI.Models;

namespace FilmesAPI.Repository
{
    public interface ISessaoRepository
    {
        Task CriarSessao(Sessao sessao);
        List<Sessao> PegarSessoesPag(int skip, int take);
        Sessao PegarSessaoPorId(int id);
        Task UpdateSessao(Sessao sessao);
        Task DeleteSessao(Sessao sessao);
    }
    public class SessaoRepository : ISessaoRepository
    {
        private readonly FilmeContext _dbContext;

        public SessaoRepository(FilmeContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        public async Task CriarSessao(Sessao sessao)
        {
            _dbContext.Sessoes.Add(sessao);
            await _dbContext.SaveChangesAsync();
        }

        public List<Sessao> PegarSessoesPag(int skip, int take)
        {
            var listaPaginada = _dbContext.Sessoes.Skip(skip).Take(take).ToList();

            return listaPaginada;
        }

        public Sessao PegarSessaoPorId(int id)
        {
            var sessao = _dbContext.Sessoes.FirstOrDefault(s => s.Id == id);

            return sessao;
        }

        public async Task UpdateSessao(Sessao sessao)
        {
            _dbContext.Sessoes.Update(sessao);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteSessao(Sessao sessao)
        {
            _dbContext.Sessoes.Remove(sessao);
            await _dbContext.SaveChangesAsync();
        }

    }
}

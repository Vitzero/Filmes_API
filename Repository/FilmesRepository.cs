using FilmesAPI.Data;
using FilmesAPI.Models;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Repository
{
    public interface IFilmesRepository
    {
        public Task AddFilme(Filme filme);
        
        public Task<List<Filme>> PegarFilmes(int skip, int take);
        Task<Filme?> ObterPorIdAsync(int id);

        public Task UpdateFilme(Filme filme);

        public Task RemoveFilme(Filme filme);
    }

    public class FilmesRepository : IFilmesRepository
    {
        private readonly FilmeContext _dbContext;

        public FilmesRepository(FilmeContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        public async Task<List<Filme>> PegarFilmes(int skip, int take)
        {
            var Lista = await _dbContext.Filmes.Skip(skip).Take(take).ToListAsync();
            return Lista;
        }

        public async Task<Filme?> ObterPorIdAsync(int id)
        {
            return await _dbContext.Filmes.FirstOrDefaultAsync(f => f.Id == id);
        }


        public async Task AddFilme(Filme filme)
        {
            _dbContext.Filmes.Add(filme);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateFilme(Filme filme)
        {
            _dbContext.Update(filme);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveFilme(Filme filme)
        {
            _dbContext.Filmes.Remove(filme);
            await _dbContext.SaveChangesAsync();
        }

    }
}

using FilmesAPI.Data;
using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Repository
{
    public interface IFilmesRepository
    {
        public Task Create(Filme filme);

        public Task<List<Filme>> GetAll(int skip, int take);

        Task<Filme?> GetById(int id);

        public Task Update(Filme filme);

        public Task Delete(Filme filme);
    }

    public class FilmesRepository : IFilmesRepository
    {
        private readonly FilmeContext _dbContext;

        public FilmesRepository(FilmeContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        public async Task<List<Filme>> GetAll(int skip, int take)
        {
            var Lista = await _dbContext.Filmes.Skip(skip).Take(take).ToListAsync();
            return Lista;
        }

        public async Task<Filme?> GetById(int id)
        {
            return await _dbContext.Filmes.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task Create(Filme filme)
        {
            _dbContext.Filmes.Add(filme);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Filme filme)
        {
            _dbContext.Update(filme);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Filme filme)
        {
            _dbContext.Filmes.Remove(filme);
            await _dbContext.SaveChangesAsync();
        }
    }
}
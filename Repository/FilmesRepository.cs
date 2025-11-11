using FilmesAPI.Data;
using FilmesAPI.Models;
using Microsoft.AspNetCore.JsonPatch.Internal;

namespace FilmesAPI.Repository
{
    public interface IFilmesRepository
    {
        public Task AddFilme(Filme filme);
        
        public List<Filme> PegarFilmes();

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

        public List<Filme> PegarFilmes()
        {
            var Lista = _dbContext.Filmes.ToList();
            return Lista;
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

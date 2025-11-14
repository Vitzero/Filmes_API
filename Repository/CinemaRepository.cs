using FilmesAPI.Data;
using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Repository
{
    public interface ICinemaRepository
    {
        Task Create(Cinema cinema);

        Task<IList<Cinema>> Get(int skip, int take);

        Task<Cinema?> GetById(int id);

        Task Update(Cinema cinema);

        Task Delete(Cinema cinema);
    }

    public class CinemaRepository : ICinemaRepository
    {
        private readonly FilmeContext context;

        public CinemaRepository(FilmeContext _context)
        {
            context = _context;
        }

        public async Task Create(Cinema cinema)
        {
            context.Cinemas.Add(cinema);
            await context.SaveChangesAsync();
        }

        public async Task<IList<Cinema>> Get(int skip, int take)
        {
            var cinemas = await context.Cinemas
                .OrderBy(x => x.Id)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return cinemas;
        }

        public async Task<Cinema?> GetById(int id)
        {
            return await context.Cinemas
                .Include(c => c.Endereco)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Update(Cinema cinema)
        {
            context.Cinemas.Update(cinema);
            await context.SaveChangesAsync();
        }

        public async Task Delete(Cinema cinema)
        {
            context.Cinemas.Remove(cinema);
            await context.SaveChangesAsync();
        }
    }
}
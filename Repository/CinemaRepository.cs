using FilmesAPI.Data;
using FilmesAPI.Models;
using Microsoft.AspNetCore.JsonPatch.Internal;

namespace FilmesAPI.Repository
{
    public interface ICinemaRepository
    {
        Task CinemaAdd(Cinema cinema);
        List<Cinema> GetCinemasPag(int skip, int take);
        Cinema GetCinemaBanco(int id);
        Task AtualizarCinema(Cinema cinema);
        Task Deletar(Cinema cinema);

    }
    
    public class CinemaRepository
    {
        private readonly FilmeContext context;

        public async Task CinemaAdd(Cinema cinema)
        {
            context.Cinemas.Add(cinema);
            await context.SaveChangesAsync();
        }
        public List<Cinema> GetCinemasPag(int skip, int take)
        {
            var cinemas = context.Cinemas
                .Skip(skip)
                .Take(take)
                .OrderBy(x=>x.Id)
                .ToList();

            return cinemas;
        }
        public async Task<Cinema?> GetCinemaBanco(int id)
        {
            return context.Cinemas.FirstOrDefault(x=>x.Id == id);
        }

        public async Task AtualizarCinema(Cinema cinema)
        {
            context.Cinemas.Update(cinema);
            await context.SaveChangesAsync();
        }

        public async Task Deletar(Cinema cinema)
        {
            context.Cinemas.Remove(cinema);
            await context.SaveChangesAsync();
        }


    }
}

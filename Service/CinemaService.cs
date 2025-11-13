using FilmesAPI.Models;
using FilmesAPI.Models.DTOs;
using FilmesAPI.Repository;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FilmesAPI.Service
{
    public interface ICinemaService
    {
        Task CriarCinema(CreateCinemaDto cinema);
        Task<List<CinemaResponseDTO>> GetCinemas(int skip, int take);
        Task<CinemaResponseDTO?> GetCinemaPorId(int id);
        Task AtualizarCinema(UpdateCinemaDto updateCinema, int id);
        Task DeleteCinema(int id);
    }

    public class CinemaService : ICinemaService
    {
        private readonly ICinemaRepository _cinemaRepository;

        public CinemaService(ICinemaRepository _cinemaRepository)
        {
            this._cinemaRepository = _cinemaRepository;
        }

        public async Task CriarCinema(CreateCinemaDto cinema)
        {

            Cinema cinemaModel = new()
            {
                Nome = cinema.Nome,
                EnderecoId = cinema.CinemaId
            };

            await _cinemaRepository.CinemaAdd(cinemaModel);
           
        }
        public async Task<List<CinemaResponseDTO>> GetCinemas(int skip, int take)
        {
            var cinemas = await _cinemaRepository.GetCinemasPag(skip, take);

            var c = cinemas.Select(c=>c.ToDto()).ToList();

            return c;

        }

        public async Task<CinemaResponseDTO?> GetCinemaPorId(int id)
        {
            Cinema? cinema = await _cinemaRepository.GetCinemaBanco(id);

            if (cinema == null)
                return null;

            return cinema.ToDto();
        }

        public async Task AtualizarCinema(UpdateCinemaDto updateCinema, int id)
        {
            var cinema = await _cinemaRepository.GetCinemaBanco(id);

            cinema.Nome = updateCinema.Nome;
            cinema.EnderecoId = updateCinema.EnderecoId;

            await _cinemaRepository.AtualizarCinema(cinema);

        }
        public async Task DeleteCinema(int id)
        {
            var cinema = await _cinemaRepository.GetCinemaBanco(id);

            await _cinemaRepository.Deletar(cinema);
        }

    }
}

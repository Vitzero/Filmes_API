using FilmesAPI.Models;
using FilmesAPI.Models.DTOs;
using FilmesAPI.Repository;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FilmesAPI.Service
{
    public interface ICinemaService
    {
        Task CriarCinema(CreateCinemaDto cinema);
        List<Cinema> GetCinemas(int skip, int take);
        CinemaResponseDTO GetCinemaPorId(int id);
        Task AtualizarCinema(UpdateCinemaDto updateCinema, int id);
        Task DeleteCinema(int id);
    }

    public class CinemaService : ICinemaService
    {
        private readonly ICinemaRepository _cinemaRepository;

        public async Task CriarCinema(CreateCinemaDto cinema)
        {

            Cinema cinemaModel = new()
            {
                Nome = cinema.Nome,
                Endereco =
                {
                     Logradouro = cinema.Endereco.Logradouro,
                     Numero = cinema.Endereco.Numero
                }
            };

            _cinemaRepository.CinemaAdd(cinemaModel);
           
        }
        public List<Cinema> GetCinemas(int skip, int take)
        {
            var cinemas = _cinemaRepository.GetCinemasPag(skip, take);

            return cinemas;

        }

        public CinemaResponseDTO GetCinemaPorId(int id)
        {
             var cinema = _cinemaRepository.GetCinemaBanco(id);

            CinemaResponseDTO response = new()
            {
                Nome = cinema.Nome,
                ReadEnderecoDto = new()
                {
                    Id = cinema.EnderecoId,
                    Logradouro = cinema.Endereco.Logradouro,
                    Numero = cinema.Endereco.Numero
                }
            };


            return response;
        }
        public async Task AtualizarCinema(UpdateCinemaDto updateCinema, int id)
        {
            var cinema = _cinemaRepository.GetCinemaBanco(id);

            cinema = updateCinema.ToEntity();

            await _cinemaRepository.AtualizarCinema(cinema);

        }
        public async Task DeleteCinema(int id)
        {
            var cinema = _cinemaRepository.GetCinemaBanco(id);

            await _cinemaRepository.Deletar(cinema);
        }

    }
}

using FilmesAPI.Models;
using FilmesAPI.Models.DTOs.Cinema;
using FilmesAPI.Repository;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FilmesAPI.Service
{
    public interface ICinemaService
    {
        Task CriarCinema(CreateCinemaDto cinema);
        List<Cinema> GetCinemas(int skip, int take);

        Cinema GetCinemaPorId(int id);
    }

    public class CinemaService : ICinemaService
    {
        private readonly ICinemaRepository _cinemaRepository;

        public async Task CriarCinema(CreateCinemaDto cinema)
        {

            Cinema cinemaModel = new()
            {
                Nome = cinema.Nome,
                EnderecoId = cinema.EnderecoId
            };

            _cinemaRepository.CinemaAdd(cinemaModel);
           
        }
        public List<Cinema> GetCinemas(int skip, int take)
        {
            var cinemas = _cinemaRepository.GetCinemasPag(skip, take);

            return cinemas;

        }

        public ReadCinemaDto GetCinemaPorId(int id)
        {
             var cinema = _cinemaRepository.GetCinemaBanco(id);

            ReadCinemaDto response = new()
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

    }
}

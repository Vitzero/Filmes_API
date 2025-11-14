using FilmesAPI.Models;
using FilmesAPI.Models.DTOs;
using FilmesAPI.Repository;

namespace FilmesAPI.Service
{
    public interface ICinemaService
    {
        Task Create(CreateCinemaDto cinema);

        Task<IList<CinemaResponseDTO>> GetAll(int skip, int take);

        Task<CinemaResponseDTO?> GetById(int id);

        Task Update(UpdateCinemaDto updateCinema, int id);

        Task Delete(int id);
    }

    public class CinemaService : ICinemaService
    {
        private readonly ICinemaRepository _cinemaRepository;

        public CinemaService(ICinemaRepository _cinemaRepository)
        {
            this._cinemaRepository = _cinemaRepository;
        }

        public async Task Create(CreateCinemaDto cinema)
        {
            Cinema cinemaModel = new()
            {
                Nome = cinema.Nome,
                EnderecoId = cinema.CinemaId
            };

            await _cinemaRepository.Create(cinemaModel);
        }

        public async Task<IList<CinemaResponseDTO>> GetAll(int skip, int take)
        {
            var cinemas = await _cinemaRepository.Get(skip, take);

            var CinemasToDto = cinemas.Select(c => c.ToDto()).ToList();

            return CinemasToDto;
        }

        public async Task<CinemaResponseDTO?> GetById(int id)
        {
            Cinema? cinema = await _cinemaRepository.GetById(id);

            if (cinema == null)
            {
                return null;
            }

            return cinema.ToDto();
        }

        public async Task Update(UpdateCinemaDto updateCinema, int id)
        {
            var cinema = await _cinemaRepository.GetById(id);

            cinema.Nome = updateCinema.Nome;
            cinema.EnderecoId = updateCinema.EnderecoId;

            await _cinemaRepository.Update(cinema);
        }

        public async Task Delete(int id)
        {
            var cinema = await _cinemaRepository.GetById(id);

            await _cinemaRepository.Delete(cinema);
        }
    }
}
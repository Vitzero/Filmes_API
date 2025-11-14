using FilmesAPI.Models;
using FilmesAPI.Models.DTOs;
using FilmesAPI.Repository;

namespace FilmesAPI.Service
{
    public interface IEnderecoService
    {
        Task Create(CreateEnderecoDto endereco);

        Task<IList<Endereco>> GetAll(int skip, int take);

        Task<Endereco> GetById(int id);

        Task Update(UpdateEnderecoDto endereco, int id);

        Task Delete(int id);
    }

    public class EnderecoService : IEnderecoService
    {
        private readonly IEnderecoRepository _enderecoRepository;

        public EnderecoService(IEnderecoRepository repository)
        {
            _enderecoRepository = repository;
        }

        public async Task Create(CreateEnderecoDto endereco)
        {
            Endereco endereroEntity = endereco.ToEntity();

            await _enderecoRepository.Create(endereroEntity);
        }

        public async Task Delete(int id)
        {
            var endereco2delet = await _enderecoRepository.GetById(id);

            if (endereco2delet != null)
            {
                await _enderecoRepository.Delete(endereco2delet);
            }
        }

        public async Task<Endereco> GetById(int id)
        {
            var embarcadoWithId = await _enderecoRepository.GetById(id);
            return embarcadoWithId;
        }

        public async Task<IList<Endereco>> GetAll(int skip, int take)
        {
            var listEnd = await _enderecoRepository.GetAll(skip, take);
            return listEnd;
        }

        public async Task Update(UpdateEnderecoDto endereco, int id)
        {
            var endereco2update = await _enderecoRepository.GetById(id);

            await _enderecoRepository.Update(endereco2update);
        }
    }
}
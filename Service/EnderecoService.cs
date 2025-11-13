using FilmesAPI.Models;
using FilmesAPI.Models.DTOs;
using FilmesAPI.Repository;

namespace FilmesAPI.Service
{
    public interface IEnderecoService
    {
        Task CreateEndereco(CreateEnderecoDto endereco);
        List<Endereco> GetEnderecos(int skip, int take);
        Endereco GetEnderecoById(int id);
        Task UpdateEndereco(UpdateEnderecoDto endereco, int id);
        Task DeleteEndereco(int id);
    }
    public class EnderecoService : IEnderecoService
    {
        private readonly IEnderecoRepository _enderecoRepository;

        public async Task CreateEndereco(CreateEnderecoDto endereco)
        {
            Endereco endereroEntity = endereco.ToEntity();

            await _enderecoRepository.AdicionarEnderecoAsync(endereroEntity);
        }

        public async Task DeleteEndereco(int id)
        {
            var endereco2delet = _enderecoRepository.ObterPorId(id);
            _enderecoRepository.RemoverAsync(endereco2delet);
        }

        public Endereco GetEnderecoById(int id)
        {
            var embarcadoWithId = _enderecoRepository.ObterPorId(id);
            return embarcadoWithId;
        }

        public List<Endereco> GetEnderecos(int skip, int take)
        {
            var listEnd = _enderecoRepository.ObterTodosAsync(skip, take);
            return listEnd;
        }

        public async Task UpdateEndereco(UpdateEnderecoDto endereco, int id)
        {
            var endereco2update = _enderecoRepository.ObterPorId(id);

            _enderecoRepository.AtualizarAsync(endereco2update);

        }
    }
}

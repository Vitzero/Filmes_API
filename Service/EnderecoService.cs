using FilmesAPI.Models;
using FilmesAPI.Models.DTOs;
using FilmesAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Service
{
    public interface IEnderecoService
    {
        Task CreateEndereco(CreateEnderecoDto endereco);
        List<Endereco> GetEnderecos(int skip, int take);
        Task<Endereco> GetEnderecoById(int id);
        Task UpdateEndereco(UpdateEnderecoDto endereco, int id);
        Task DeleteEndereco(int id);
    }
    public class EnderecoService : IEnderecoService
    {
        private readonly IEnderecoRepository _enderecoRepository;

        public EnderecoService(IEnderecoRepository repository)
        {
            _enderecoRepository = repository;
        }

        public async Task CreateEndereco(CreateEnderecoDto endereco)
        {
            Endereco endereroEntity = endereco.ToEntity();

            await _enderecoRepository.AdicionarEnderecoAsync(endereroEntity);
        }

        public async Task DeleteEndereco(int id)
        {
            var endereco2delet = await _enderecoRepository.ObterPorId(id);

            if (endereco2delet != null)
            {
                await _enderecoRepository.RemoverAsync(endereco2delet);
            }

            
        }

        public async Task<Endereco> GetEnderecoById(int id)
        {
            var embarcadoWithId = await _enderecoRepository.ObterPorId(id);
            return embarcadoWithId;
        }

        public List<Endereco> GetEnderecos(int skip, int take)
        {
            var listEnd = _enderecoRepository.ObterTodosAsync(skip, take);
            return listEnd;
        }

        public async Task UpdateEndereco(UpdateEnderecoDto endereco, int id)
        {
            var endereco2update = await _enderecoRepository.ObterPorId(id);

            await _enderecoRepository.AtualizarAsync(endereco2update);

        }
    }
}

using FilmesAPI.Data;
using FilmesAPI.Models;
using FilmesAPI.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Repository
{
    public interface IEnderecoRepository
    {
        Task AdicionarEnderecoAsync(Endereco endereco);
        List<Endereco> ObterTodosAsync(int skip, int take);
        Task<Endereco> ObterPorId(int id);
        Task AtualizarAsync(Endereco endereco);
        Task RemoverAsync(Endereco id);
    }
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly FilmeContext _dbContext;

        public EnderecoRepository(FilmeContext context)
        {
            _dbContext = context;
        }

        public async Task AdicionarEnderecoAsync(Endereco endereco)
        {
            _dbContext.Enderecos.Add(endereco);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Endereco endereco)
        {
            _dbContext.Enderecos.Update(endereco);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Endereco?> ObterPorId(int id)
        {
            return await _dbContext.Enderecos.FirstOrDefaultAsync(en => en.Id == id);
        }


        public List<Endereco> ObterTodosAsync(int skip, int take)
        {
            List<Endereco> listaEnderecos = _dbContext.Enderecos
                .Skip(skip)
                .Take(take)
                .ToList();
            return listaEnderecos;
        }

        public async Task RemoverAsync(Endereco endereco)
        {
            _dbContext.Enderecos.Remove(endereco);
            await _dbContext.SaveChangesAsync();
        }
    }
}

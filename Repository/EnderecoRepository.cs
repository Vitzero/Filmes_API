using FilmesAPI.Data;
using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Repository
{
    public interface IEnderecoRepository
    {
        Task Create(Endereco endereco);

        Task<IList<Endereco>> GetAll(int skip, int take);

        Task<Endereco?> GetById(int id);

        Task Update(Endereco endereco);

        Task Delete(Endereco id);
    }

    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly FilmeContext _dbContext;

        public EnderecoRepository(FilmeContext context)
        {
            _dbContext = context;
        }

        public async Task Create(Endereco endereco)
        {
            _dbContext.Enderecos.Add(endereco);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Endereco endereco)
        {
            _dbContext.Enderecos.Update(endereco);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Endereco?> GetById(int id)
        {
            return await _dbContext.Enderecos.FirstOrDefaultAsync(en => en.Id == id);
        }

        public async Task<IList<Endereco>> GetAll(int skip, int take)
        {
            List<Endereco> listaEnderecos = await _dbContext.Enderecos
                .Skip(skip)
                .Take(take)
                .ToListAsync();
            return listaEnderecos;
        }

        public async Task Delete(Endereco endereco)
        {
            _dbContext.Enderecos.Remove(endereco);
            await _dbContext.SaveChangesAsync();
        }
    }
}
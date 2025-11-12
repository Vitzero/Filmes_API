using FilmesAPI.Data;
using FilmesAPI.Models;
using FilmesAPI.Models.DTOs;

namespace FilmesAPI.Repository
{
    public interface IEnderecoRepository
    {
        Task AdicionarEnderecoAsync(Endereco endereco);
        List<Endereco> ObterTodosAsync(int skip, int take);
        Endereco ObterPorId(int id);
        Task AtualizarAsync(Endereco endereco);
        Task RemoverAsync(Endereco id);
    }
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly FilmeContext _dbContext;

        public async Task AdicionarEnderecoAsync(Endereco endereco)
        {
            _dbContext.Enderecos.Add(endereco);
            _dbContext.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Endereco endereco)
        {
            _dbContext.Enderecos.Update(endereco);
            _dbContext.SaveChangesAsync();
        }

        public Endereco ObterPorId(int id)
        {
           var endereco = _dbContext.Enderecos.FirstOrDefault(en => en.Id == id);
            return endereco;
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
        }
    }
}

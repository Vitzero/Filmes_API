using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Data;

public class FilmeContext : DbContext
{
    public FilmeContext(DbContextOptions<FilmeContext> opts): base(opts)
    {
        
    }

    // pega o a CLASSE filme, onde colocamos os campos required e KEY, para levar para uma tabela no banco automaticamente
    public DbSet<Filme> Filmes { get; set; }

}

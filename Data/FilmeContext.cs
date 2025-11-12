using FilmesAPI.Data.Configuration;
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
    public DbSet<Cinema> Cinemas { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<Sessao> Sessoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CinemaTypeConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EnderecoTypeConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SessaoTypeConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FilmesTypeConfiguration).Assembly);
    }


}

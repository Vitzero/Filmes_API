using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmesAPI.Data.Configuration
{
    public class FilmesTypeConfiguration : IEntityTypeConfiguration<Filme>
    {
        public void Configure(EntityTypeBuilder<Filme> builder)
        {
            builder.ToTable("filmes");

            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasColumnType("int")
                .HasColumnName("id")
                .IsRequired();

            builder
                .Property(x => x.Titulo)
                .HasColumnType("varchar(200)")
                .HasColumnName("titulo")
                .IsRequired();

            builder
                .Property(x => x.Duracao)
                .HasColumnType("int")
                .HasColumnName("duracao")
                .IsRequired();

            builder
                .Property(x => x.Genero)
                .HasColumnType("varchar(100)")
                .HasColumnName("genero")
                .IsRequired();
        }
    }
}
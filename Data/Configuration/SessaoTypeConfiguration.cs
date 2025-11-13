using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmesAPI.Data.Configuration
{
    public class SessaoTypeConfiguration : IEntityTypeConfiguration<Sessao>
    {
        public void Configure(EntityTypeBuilder<Sessao> builder)
        {
            builder.ToTable("Sessoes");

            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasColumnType("int")
                .HasColumnName("id")
                .IsRequired();

            builder
                .Property(x => x.CinemaId)
                .HasColumnType("int")
                .HasColumnName("cinema_id")
                .IsRequired();


            builder
                .Property(x => x.FilmeId)
                .HasColumnType("int")
                .HasColumnName("filme_id")
                .IsRequired();


            // RELACIONAMENTO

            builder
                .HasOne<Filme>(x => x.Filme)
                .WithMany(f => f.Sessoes)
                .HasForeignKey(x => x.FilmeId)
                .IsRequired();

            builder
                .HasOne<Cinema>(x => x.Cinema)
                .WithMany()
                .HasForeignKey(x => x.CinemaId)
                .IsRequired();

        }
    }
}

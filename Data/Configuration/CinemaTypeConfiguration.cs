using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmesAPI.Data.Configuration;


public class CinemaTypeConfiguration : IEntityTypeConfiguration<Cinema>
{
    public void Configure(EntityTypeBuilder<Cinema> builder)
    {
        builder.ToTable("cinemas");

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasColumnType("bigint")
            .HasColumnName("id")
            .IsRequired();

        builder
            .Property(x => x.Nome)
            .HasColumnType("varchar(60)")
            .HasColumnName("id")
            .IsRequired();

        builder
            .HasOne(x => x.Endereco)
            .WithOne()
            .HasForeignKey<Cinema>(x => x.EnderecoId) // pai (cinema) possui o children pela fk (endereco)
            .IsRequired();
    }

}

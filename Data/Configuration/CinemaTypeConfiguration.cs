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
            .HasColumnType("int")
            .HasColumnName("id")
            .IsRequired();

        builder
            .Property(x => x.Nome)
            .HasColumnType("varchar(80)")
            .HasColumnName("nome")
            .IsRequired();

        builder
            .Property(x => x.EnderecoId)
            .HasColumnType("int") // ou bigint se Endereco.Id for bigint
            .HasColumnName("endereco_id")
            .IsRequired();

        builder
            .HasOne(x => x.Endereco)
            .WithOne()
            .HasForeignKey<Cinema>(x => x.EnderecoId) // pai (cinema) possui o children pela fk (endereco)
            .IsRequired();
    }
}
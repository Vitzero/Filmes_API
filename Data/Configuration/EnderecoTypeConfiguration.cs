using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmesAPI.Data.Configuration;

public class EnderecoTypeConfiguration : IEntityTypeConfiguration<Endereco>
{
    public void Configure(EntityTypeBuilder<Endereco> builder)
    {
        builder.ToTable("Enderecos");

        builder.HasKey(e => e.Id);

        builder
            .Property(e => e.Id)
            .HasColumnType("bigint")
            .HasColumnName("id")
            .IsRequired();

        builder
            .Property(e => e.Numero)
            .HasColumnType("bigint")
            .HasColumnName("Numero")
            .IsRequired();

        builder
            .Property(e => e.Logradouro)
            .HasColumnType("varchar(300)")
            .HasColumnName("Logradouro")
            .IsRequired();
    }
}

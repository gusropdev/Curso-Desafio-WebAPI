using desafio_teste.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace desafio_teste.Data.Mappings;

public class AlunoMap : IEntityTypeConfiguration<Aluno>
{
    public void Configure(EntityTypeBuilder<Aluno> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired()
            .UseIdentityColumn(1, 1)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Nome)
             .IsRequired()
             .HasColumnName("Nome")
             .HasColumnType("NVARCHAR")
             .HasMaxLength(80);

        builder.Property(x => x.Cpf)
            .IsRequired()
            .HasColumnName("CPF")
            .HasColumnType("VARCHAR")
            .HasMaxLength(15);

        builder.Property(x => x.Email)
             .IsRequired()
             .HasColumnName("Email")
             .HasColumnType("VARCHAR")
             .HasMaxLength(80);

        builder.HasIndex(x => x.Cpf)
            .IsUnique();
    }
}
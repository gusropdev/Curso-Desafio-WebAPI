using desafio_teste.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace desafio_teste.Data.Mappings;

public class TurmaMap : IEntityTypeConfiguration<Turma>
{
    public void Configure(EntityTypeBuilder<Turma> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired()
            .UseIdentityColumn(1001, 1)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Codigo)
            .IsRequired()
            .HasColumnName("Código")
            .HasColumnType("INT");

        builder.Property(x => x.Nivel)
            .IsRequired()
            .HasColumnName("Nível")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(15);

        builder.HasIndex(x => x.Codigo)
            .IsUnique();

        builder.HasMany(x => x.Alunos)
            .WithMany(x => x.Turmas)
            .UsingEntity<Dictionary<string, object>>(
                "TurmaAluno",
                turma => turma.HasOne<Aluno>()
                            .WithMany()
                            .HasForeignKey("AlunoId")
                            .HasConstraintName("FK_TurmaAluno_AlunoId")
                            .OnDelete(DeleteBehavior.Cascade),

                aluno => aluno.HasOne<Turma>()
                            .WithMany()
                            .HasForeignKey("TurmaId")
                            .HasConstraintName("FK_TurmAluno_TurmaId")
                            .OnDelete(DeleteBehavior.Cascade)
            );
    }
}

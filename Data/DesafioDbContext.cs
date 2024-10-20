using desafio_teste.Data.Mappings;
using desafio_teste.Models;
using Microsoft.EntityFrameworkCore;

namespace desafio_teste.Data;

public class DesafioDbContext : DbContext
{
    public DesafioDbContext(DbContextOptions<DesafioDbContext> options) : base(options) { }

    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<Turma> Turmas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AlunoMap());
        modelBuilder.ApplyConfiguration(new TurmaMap());
    }
}

﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using desafio_teste.Data;

#nullable disable

namespace desafio_teste.Migrations
{
    [DbContext(typeof(DesafioDbContext))]
    [Migration("20241020141810_CreateDatabase")]
    partial class CreateDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("TurmaAluno", b =>
                {
                    b.Property<int>("AlunoId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TurmaId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AlunoId", "TurmaId");

                    b.HasIndex("TurmaId");

                    b.ToTable("TurmaAluno");
                });

            modelBuilder.Entity("desafio_teste.Models.Aluno", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1L)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("CPF");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Email");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Nome");

                    b.HasKey("Id");

                    b.HasIndex("Cpf")
                        .IsUnique();

                    b.ToTable("Alunos");
                });

            modelBuilder.Entity("desafio_teste.Models.Turma", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1001L)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Codigo")
                        .HasColumnType("INT")
                        .HasColumnName("Código");

                    b.Property<string>("Nivel")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Nível");

                    b.HasKey("Id");

                    b.HasIndex("Codigo")
                        .IsUnique();

                    b.ToTable("Turmas");
                });

            modelBuilder.Entity("TurmaAluno", b =>
                {
                    b.HasOne("desafio_teste.Models.Aluno", null)
                        .WithMany()
                        .HasForeignKey("AlunoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_TurmaAluno_AlunoId");

                    b.HasOne("desafio_teste.Models.Turma", null)
                        .WithMany()
                        .HasForeignKey("TurmaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_TurmAluno_TurmaId");
                });
#pragma warning restore 612, 618
        }
    }
}

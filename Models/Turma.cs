namespace desafio_teste.Models;

public class Turma
{
    public int Id { get; set; }
    public int Codigo { get; set; }
    public string Nivel { get; set; }

    public ICollection<Aluno> Alunos { get; set; } = new List<Aluno>();
}

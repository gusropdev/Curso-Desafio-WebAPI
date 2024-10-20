namespace desafio_teste.Models;

public class Aluno
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public string Email { get; set; }

    public ICollection<Turma> Turmas { get; set; } = new List<Turma>();
}

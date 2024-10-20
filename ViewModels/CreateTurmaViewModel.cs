using System.ComponentModel.DataAnnotations;

namespace desafio_teste.ViewModels;

public class CreateTurmaViewModel
{

    [Required(ErrorMessage = "O código é obrigatório.")]
    public int Codigo { get; set; }

    [Required(ErrorMessage = "O nível é obrigatório.")]
    [MaxLength(15, ErrorMessage = "O nível ultrapassou o limite máximo de caracteres.")]
    public string Nivel { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace desafio_teste.ViewModels;

public class EditAlunoViewModel
{
    [Required]
    public string Nome { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "E-mail inválido.")]
    public string Email { get; set; }
}

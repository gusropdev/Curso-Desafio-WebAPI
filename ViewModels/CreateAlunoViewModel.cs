using System.ComponentModel.DataAnnotations;

namespace desafio_teste.ViewModels;

public class CreateAlunoViewModel
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O CPF é obrigatório")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve ter 11 dígitos.")]
    public string Cpf { get; set; }

    [EmailAddress(ErrorMessage = "E-mail inválido.")]
    [Required(ErrorMessage = "E-mail obrigatório.")]
    public string Email { get; set; }
}

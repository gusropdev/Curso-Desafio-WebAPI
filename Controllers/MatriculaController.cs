using desafio_teste.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace desafio_teste.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MatriculaController : ControllerBase
{
    private readonly DesafioDbContext _context;

    public MatriculaController(DesafioDbContext context) => _context = context;


    [HttpPost("cadastrar")]
    public async Task<IActionResult> RealizarMatriculaAsync([FromQuery] int alunoId, [FromQuery] int turmaId)
    {
        var aluno = await _context.Alunos.FirstOrDefaultAsync(x => x.Id == alunoId);
        var turma = await _context.Turmas.Include(x => x.Alunos).FirstOrDefaultAsync(x => x.Id == turmaId);

        if (turma == null || aluno == null)
            return NotFound(new { message = "Aluno ou turma não encontrados." });

        if (turma.Alunos.Contains(aluno))
            return BadRequest(new { message = "O aluno já está matriculado nessa turma.", aluno });

        if (turma.Alunos.Count() >= 5)
            return BadRequest(new { message = "A turma já atingiu o limite máximo de 5 alunos." });

        try
        {
            turma.Alunos.Add(aluno);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Aluno matriculado com sucesso.", aluno, turma });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("remover")]
    public async Task<IActionResult> RemoverMatriculaAsync([FromQuery] int alunoId, [FromQuery] int turmaId)
    {
        var aluno = await _context.Alunos.FirstOrDefaultAsync(x => x.Id == alunoId);
        var turma = await _context.Turmas.Include(x => x.Alunos).FirstOrDefaultAsync(x => x.Id == turmaId);

        if (turma == null || aluno == null)
            return NotFound(new { message = "Aluno ou turma não encontrados." });

        if (!turma.Alunos.Contains(aluno))
            return BadRequest(new { message = "Este aluno não está matriculado nessa turma." });

        try
        {
            turma.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Aluno desmatriculado com sucesso.", aluno, turma });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

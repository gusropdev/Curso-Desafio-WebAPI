using desafio_teste.Data;
using desafio_teste.Models;
using desafio_teste.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace desafio_teste.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TurmaController : ControllerBase
{
    private readonly DesafioDbContext _context;

    public TurmaController(DesafioDbContext context) => _context = context;

    [HttpGet("listagem")]
    public async Task<IActionResult> GetTurmasAsync(
        [FromQuery] string? nivel = null,
        [FromQuery] int? codigo = null,
        [FromQuery] int? id = null,
        [FromQuery] int? maxAlunos = null,
        [FromQuery] int? minAlunos = null)
    {
        try
        {
            var query = _context.Turmas.AsQueryable();

            if (!string.IsNullOrEmpty(nivel))
                query = query.Where(x => x.Nivel == nivel);

            if (codigo.HasValue)
                query = query.Where(x => x.Codigo == codigo);

            if (id.HasValue)
                query = query.Where(x => x.Id == id);

            if (maxAlunos.HasValue)
                query = query.Where(x => x.Alunos.Count <= maxAlunos);

            if (minAlunos.HasValue)
                query = query.Where(x => x.Alunos.Count >= minAlunos);

            //modifica a Turma retornada para quee contenha uma lista do CPF dos Alunos matriculados
            var turmas = await query.Select(x => new
            {
                x.Id,
                x.Codigo,
                x.Nivel,
                Alunos = x.Alunos.Select(y => y.Cpf).ToList()
            }).ToListAsync();

            return Ok(turmas);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("cadastrar")]
    public async Task<IActionResult> CreateTurmaAsync([FromBody] CreateTurmaViewModel turmaModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (await _context.Turmas.AnyAsync(x => x.Codigo == turmaModel.Codigo))
            return BadRequest(new { message = "Código de turma já existente." });

        var turma = new Turma
        {
            Codigo = turmaModel.Codigo,
            Nivel = turmaModel.Nivel,
        };

        try
        {
            await _context.Turmas.AddAsync(turma);
            await _context.SaveChangesAsync();

            return Created($"api/turma/{turma.Id}", new { message = "Turma cadastrada com sucesso", turma });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("remover/{id:int}")]
    public async Task<IActionResult> RemoveTurmaAsync([FromRoute] int id)
    {
        var turma = await _context.Turmas.Include(x => x.Alunos).FirstOrDefaultAsync(x => x.Id == id);

        if (turma == null)
            return NotFound(new { message = "Turma não encontrada." });

        if (turma.Alunos.Any())
            return BadRequest(new { message = "Não é permitido remover uma turma com alunos cadastrados." });

        try
        {
            _context.Turmas.Remove(turma);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Turma removida com sucesso", turma });
        }

        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

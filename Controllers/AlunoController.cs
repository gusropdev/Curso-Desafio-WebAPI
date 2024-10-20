using desafio_teste.Data;
using desafio_teste.Models;
using desafio_teste.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace desafio_teste.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlunoController : ControllerBase
{
    private readonly DesafioDbContext _context;

    public AlunoController(DesafioDbContext context) => _context = context;

    [HttpGet("listagem")]
    public async Task<IActionResult> GetAlunosAsync(
        [FromQuery] string? nome = null,
        [FromQuery] string? cpf = null,
        [FromQuery] string? email = null,
        [FromQuery] int? turmaCodigo = null,
        [FromQuery] int? id = null)
    {
        try
        {
            var query = _context.Alunos.AsQueryable();

            if (!string.IsNullOrEmpty(nome))
                query = query.Where(x => x.Nome.Contains(nome));

            if (!string.IsNullOrEmpty(cpf))
                query = query.Where(x => x.Cpf == cpf);

            if (!string.IsNullOrEmpty(email))
                query = query.Where(x => x.Email.Contains(email));

            if (turmaCodigo.HasValue)
                query = query.Where(x => x.Turmas.Any(y => y.Codigo == turmaCodigo));

            if (id.HasValue)
                query = query.Where(x => x.Id == id);

            //Modifica o Aluno retornado para que contenha uma lista das turmas em que o aluno está matriculado
            var alunos = await query.Select(x => new
            {
                x.Id,
                x.Nome,
                x.Cpf,
                x.Email,
                Turmas = x.Turmas.Select(y => y.Codigo).ToList()
            }).ToListAsync();

            return Ok(alunos);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPost("cadastrar")]
    public async Task<IActionResult> CreateAlunoAsync([FromBody] CreateAlunoViewModel alunoModel, [FromQuery] int turmaCodigo)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (await _context.Alunos.AnyAsync(x => x.Cpf == alunoModel.Cpf))
            return BadRequest(new { message = "CPF já cadastrado" });


        var turma = await _context.Turmas.Include(x => x.Alunos).FirstOrDefaultAsync(x => x.Codigo == turmaCodigo);

        if (turma == null)
            return NotFound(new { message = "Turma não encontrada ou não informada." });

        if (turma.Alunos.Count >= 5)
            return BadRequest(new { message = "A turma já alcançou o limite de 5 alunos matriculados." });

        var aluno = new Aluno
        {
            Nome = alunoModel.Nome,
            Email = alunoModel.Email,
            Cpf = alunoModel.Cpf,
        };

        try
        {
            await _context.Alunos.AddAsync(aluno);
            await _context.SaveChangesAsync();

            turma.Alunos.Add(aluno);
            await _context.SaveChangesAsync();

            return Created($"api/aluno/{aluno.Id}", new { message = "Aluno cadastrado com sucesso.", aluno });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("editar/{id:int}")]
    public async Task<IActionResult> EditAlunoAsync([FromRoute] int id, [FromBody] EditAlunoViewModel alunoModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var alunoExistente = await _context.Alunos.FirstOrDefaultAsync(x => x.Id == id);
        if (alunoExistente == null)
            return NotFound(new { message = "Aluno não foi encontrado." });

        try
        {
            alunoExistente.Nome = alunoModel.Nome;
            alunoExistente.Email = alunoModel.Email;

            _context.Alunos.Update(alunoExistente);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Campos alterados com sucesso", alunoExistente });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("remover/{id:int}")]
    public async Task<IActionResult> DeleteAlunoAsync([FromRoute] int id)
    {
        var aluno = await _context.Alunos.FirstOrDefaultAsync(x => x.Id == id);
        if (aluno == null)
            return NotFound(new { message = "Aluno não encontrado" });

        try
        {
            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Aluno removido com sucesso.", aluno });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
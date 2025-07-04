using ApiCursos.Data;
using ApiCursos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class CursosController : ControllerBase
{
    private readonly AppDbContext _context;

    public CursosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Curso>>> GetCursos() =>
        await _context.Cursos.Include(c => c.Docente).ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Curso>> GetCurso(int id)
    {
        var curso = await _context.Cursos.Include(c => c.Docente).FirstOrDefaultAsync(c => c.Id == id);
        return curso == null ? NotFound() : curso;
    }

    [HttpGet("ciclo/{ciclo}")]
    public async Task<ActionResult<IEnumerable<Curso>>> GetCursosPorCiclo(int ciclo) =>
        await _context.Cursos.Where(c => c.Ciclo == ciclo).ToListAsync();

    [HttpPost]
    public async Task<ActionResult<Curso>> PostCurso(Curso curso)
    {
        _context.Cursos.Add(curso);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetCurso), new { id = curso.Id }, curso);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCurso(int id, Curso curso)
    {
        if (id != curso.Id) return BadRequest();
        _context.Entry(curso).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCurso(int id)
    {
        var curso = await _context.Cursos.FindAsync(id);
        if (curso == null) return NotFound();
        _context.Cursos.Remove(curso);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}

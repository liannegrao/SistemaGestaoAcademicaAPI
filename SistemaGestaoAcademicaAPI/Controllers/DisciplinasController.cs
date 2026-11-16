using Microsoft.AspNetCore.Mvc;
using SistemaGestaoAcademicaAPI.Models;
using SistemaGestaoAcademicaAPI.Repositories.Interfaces;

namespace SistemaGestaoAcademicaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DisciplinasController : ControllerBase
    {
        private readonly IDisciplinaRepository _repository;

        public DisciplinasController(IDisciplinaRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Disciplinas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Disciplina>>> GetDisciplinas()
        {
            var disciplinas = await _repository.ObterTodosAsync();

            return Ok(disciplinas);
        }

        // GET: api/Disciplinas/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Disciplina>> GetDisciplina(int id)
        {
            var disciplina = await _repository.ObterPorIdAsync(id);

            if (disciplina == null)
            {
                return NotFound("Disciplina não encontrada.");
            }

            return Ok(disciplina);
        }

        // POST: api/Disciplinas
        [HttpPost]
        public async Task<ActionResult<Disciplina>> PostDisciplina(
            Disciplina disciplina)
        {
            var novaDisciplina = await _repository.AdicionarAsync(disciplina);

            return CreatedAtAction(
                nameof(GetDisciplina),
                new { id = novaDisciplina.Id },
                novaDisciplina
            );
        }

        // PUT: api/Disciplinas/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDisciplina(
            int id,
            Disciplina disciplina)
        {
            if (id != disciplina.Id)
            {
                return BadRequest(
                    "O ID informado é diferente da disciplina.");
            }

            var atualizado = await _repository.AtualizarAsync(id, disciplina);

            if (!atualizado)
            {
                return NotFound("Disciplina não encontrada.");
            }

            return NoContent();
        }

        // DELETE: api/Disciplinas/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDisciplina(int id)
        {
            var excluido = await _repository.ExcluirAsync(id);

            if (!excluido)
            {
                return NotFound("Disciplina não encontrada.");
            }

            return NoContent();
        }
    }
}
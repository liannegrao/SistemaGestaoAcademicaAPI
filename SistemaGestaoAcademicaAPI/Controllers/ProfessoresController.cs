using Microsoft.AspNetCore.Mvc;
using SistemaGestaoAcademicaAPI.Models;
using SistemaGestaoAcademicaAPI.Repositories.Interfaces;

namespace SistemaGestaoAcademicaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessoresController : ControllerBase
    {
        private readonly IProfessorRepository _repository;

        public ProfessoresController(IProfessorRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Professores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Professor>>> GetProfessores()
        {
            var professores = await _repository.ObterTodosAsync();

            return Ok(professores);
        }

        // GET: api/Professores/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Professor>> GetProfessor(int id)
        {
            var professor = await _repository.ObterPorIdAsync(id);

            if (professor == null)
            {
                return NotFound("Professor não encontrado.");
            }

            return Ok(professor);
        }

        // POST: api/Professores
        [HttpPost]
        public async Task<ActionResult<Professor>> PostProfessor(Professor professor)
        {
            var novoProfessor = await _repository.AdicionarAsync(professor);

            return CreatedAtAction(
                nameof(GetProfessor),
                new { id = novoProfessor.Id },
                novoProfessor
            );
        }

        // PUT: api/Professores/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfessor(int id, Professor professor)
        {
            if (id != professor.Id)
            {
                return BadRequest("O ID informado é diferente do professor.");
            }

            var atualizado = await _repository.AtualizarAsync(id, professor);

            if (!atualizado)
            {
                return NotFound("Professor não encontrado.");
            }

            return NoContent();
        }

        // DELETE: api/Professores/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfessor(int id)
        {
            var excluido = await _repository.ExcluirAsync(id);

            if (!excluido)
            {
                return NotFound("Professor não encontrado.");
            }

            return NoContent();
        }
    }
}
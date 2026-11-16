
using Microsoft.AspNetCore.Mvc;
using SistemaGestaoAcademicaAPI.DTOs;
using SistemaGestaoAcademicaAPI.Models;
using SistemaGestaoAcademicaAPI.Repositories.Interfaces;

namespace SistemaGestaoAcademicaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TurmasController : ControllerBase
    {
        private readonly ITurmaRepository _repository;
        private readonly IProfessorRepository _professorRepository;
        private readonly IDisciplinaRepository _disciplinaRepository;

        public TurmasController(
            ITurmaRepository repository,
            IProfessorRepository professorRepository,
            IDisciplinaRepository disciplinaRepository)
        {
            _repository = repository;
            _professorRepository = professorRepository;
            _disciplinaRepository = disciplinaRepository;
        }

        // GET: api/Turmas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Turma>>> GetTurmas()
        {
            var turmas = await _repository.ObterTodosAsync();
            return Ok(turmas);
        }

        // GET: api/Turmas/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Turma>> GetTurma(int id)
        {
            var turma = await _repository.ObterPorIdAsync(id);

            if (turma == null)
                return NotFound("Turma não encontrada.");

            return Ok(turma);
        }

        // POST: api/Turmas
        [HttpPost]
        public async Task<ActionResult<Turma>> PostTurma(TurmaDTO dto)
        {
            var professor = await _professorRepository
                .ObterPorIdAsync(dto.ProfessorId);

            if (professor == null)
                return BadRequest("Professor não encontrado.");

            var disciplina = await _disciplinaRepository
                .ObterPorIdAsync(dto.DisciplinaId);

            if (disciplina == null)
                return BadRequest("Disciplina não encontrada.");

            var turma = new Turma
            {
                Nome = dto.Nome,
                ProfessorId = dto.ProfessorId,
                DisciplinaId = dto.DisciplinaId
            };

            var novaTurma = await _repository.AdicionarAsync(turma);

            return CreatedAtAction(
                nameof(GetTurma),
                new { id = novaTurma.Id },
                novaTurma
            );
        }

        // PUT: api/Turmas/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTurma(int id, TurmaDTO dto)
        {
            var turmaExistente = await _repository.ObterPorIdAsync(id);

            if (turmaExistente == null)
                return NotFound("Turma não encontrada.");

            var professor = await _professorRepository
                .ObterPorIdAsync(dto.ProfessorId);

            if (professor == null)
                return BadRequest("Professor não encontrado.");

            var disciplina = await _disciplinaRepository
                .ObterPorIdAsync(dto.DisciplinaId);

            if (disciplina == null)
                return BadRequest("Disciplina não encontrada.");

            var turma = new Turma
            {
                Id = id,
                Nome = dto.Nome,
                ProfessorId = dto.ProfessorId,
                DisciplinaId = dto.DisciplinaId
            };

            await _repository.AtualizarAsync(id, turma);

            return NoContent();
        }

        // DELETE: api/Turmas/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTurma(int id)
        {
            var excluido = await _repository.ExcluirAsync(id);

            if (!excluido)
                return NotFound("Turma não encontrada.");

            return NoContent();
        }
    }
}

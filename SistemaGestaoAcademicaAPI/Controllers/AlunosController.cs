using Microsoft.AspNetCore.Mvc;
using SistemaGestaoAcademicaAPI.Models;
using SistemaGestaoAcademicaAPI.Repositories.Interfaces;

namespace SistemaGestaoAcademicaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunosController : ControllerBase
    {
        private readonly IAlunoRepository _repository;

        public AlunosController(IAlunoRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Alunos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aluno>>> GetAlunos()
        {
            var alunos = await _repository.ObterTodosAsync();

            return Ok(alunos);
        }

        // GET: api/Alunos/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Aluno>> GetAluno(int id)
        {
            var aluno = await _repository.ObterPorIdAsync(id);

            if (aluno == null)
            {
                return NotFound("Aluno não encontrado.");
            }

            return Ok(aluno);
        }

        // POST: api/Alunos
        [HttpPost]
        public async Task<ActionResult<Aluno>> PostAluno(Aluno aluno)
        {
            var novoAluno = await _repository.AdicionarAsync(aluno);

            return CreatedAtAction(
                nameof(GetAluno),
                new { id = novoAluno.Id },
                novoAluno
            );
        }

        // PUT: api/Alunos/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAluno(int id, Aluno aluno)
        {
            if (id != aluno.Id)
            {
                return BadRequest("O ID informado é diferente do aluno.");
            }

            var atualizado = await _repository.AtualizarAsync(id, aluno);

            if (!atualizado)
            {
                return NotFound("Aluno não encontrado.");
            }

            return NoContent();
        }

        // DELETE: api/Alunos/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAluno(int id)
        {
            var excluido = await _repository.ExcluirAsync(id);

            if (!excluido)
            {
                return NotFound("Aluno não encontrado.");
            }

            return NoContent();
        }
    }
}
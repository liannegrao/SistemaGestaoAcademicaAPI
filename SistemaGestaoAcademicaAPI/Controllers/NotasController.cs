
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SistemaGestaoAcademicaAPI.DTOs;
using SistemaGestaoAcademicaAPI.Models;
using SistemaGestaoAcademicaAPI.Repositories.Interfaces;
using SistemaGestaoAcademicaAPI.Services;

namespace SistemaGestaoAcademicaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotasController : ControllerBase
    {
        private readonly INotaRepository _notaRepository;
        private readonly IAlunoRepository _alunoRepository;
        private readonly ITurmaRepository _turmaRepository;
        private readonly IMediaService _mediaService;

        public NotasController(
            INotaRepository notaRepository,
            IAlunoRepository alunoRepository,
            ITurmaRepository turmaRepository,
            IMediaService mediaService)
        {
            _notaRepository = notaRepository;
            _alunoRepository = alunoRepository;
            _turmaRepository = turmaRepository;
            _mediaService = mediaService;
        }

        // Formatar os dados retornados pela API
        private static object FormatarNota(Nota nota)
        {
            return new
            {
                nota.Id,
                nota.AlunoId,
                NomeAluno = nota.Aluno?.Nome,
                nota.TurmaId,
                NomeTurma = nota.Turma?.Nome,
                nota.Valor
            };
        }

        // GET: api/Notas
        [HttpGet]
        public async Task<IActionResult> GetNotas()
        {
            var notas = await _notaRepository.ObterTodosAsync();

            return Ok(notas.Select(FormatarNota).ToList());
        }

        // GET: api/Notas/1
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetNota(int id)
        {
            var nota = await _notaRepository.ObterPorIdAsync(id);

            if (nota == null)
            {
                return NotFound("Nota não encontrada.");
            }

            return Ok(FormatarNota(nota));
        }

        // GET: api/Notas/aluno/1
        [HttpGet("aluno/{alunoId:int}")]
        public async Task<IActionResult> GetNotasAluno(int alunoId)
        {
            var aluno = await _alunoRepository.ObterPorIdAsync(alunoId);

            if (aluno == null)
            {
                return NotFound("Aluno não encontrado.");
            }

            var notas = await _notaRepository.ObterPorAlunoAsync(alunoId);

            return Ok(notas.Select(FormatarNota).ToList());
        }

        // GET: api/Notas/aluno/1/media
        [HttpGet("aluno/{alunoId:int}/media")]
        public async Task<IActionResult> GetMediaAluno(int alunoId)
        {
            // Verificar se o aluno existe
            var aluno = await _alunoRepository.ObterPorIdAsync(alunoId);

            if (aluno == null)
            {
                return NotFound("Aluno não encontrado.");
            }

            // Calcular a média utilizando o MediaService
            var resultado = await _mediaService
                .CalcularMediaAlunoAsync(alunoId);

            // Retornar a média e a situação do aluno
            return Ok(new
            {
                resultado.AlunoId,
                NomeAluno = aluno.Nome,
                resultado.QuantidadeNotas,
                resultado.Media,
                resultado.Situacao
            });
        }

        // POST: api/Notas
        [HttpPost]
        public async Task<IActionResult> PostNota(NotaDTO dto)
        {
            var aluno = await _alunoRepository.ObterPorIdAsync(dto.AlunoId);

            if (aluno == null)
            {
                return BadRequest("Aluno não encontrado.");
            }

            var turma = await _turmaRepository.ObterPorIdAsync(dto.TurmaId);

            if (turma == null)
            {
                return BadRequest("Turma não encontrada.");
            }

            var nota = new Nota
            {
                AlunoId = dto.AlunoId,
                TurmaId = dto.TurmaId,
                Valor = dto.Valor!.Value
            };

            var novaNota = await _notaRepository.AdicionarAsync(nota);

            return CreatedAtAction(
                nameof(GetNota),
                new { id = novaNota.Id },
                new
                {
                    novaNota.Id,
                    novaNota.AlunoId,
                    novaNota.TurmaId,
                    novaNota.Valor
                }
            );
        }

        // PUT: api/Notas/1
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutNota(int id, NotaDTO dto)
        {
            var notaExistente = await _notaRepository.ObterPorIdAsync(id);

            if (notaExistente == null)
            {
                return NotFound("Nota não encontrada.");
            }

            var aluno = await _alunoRepository.ObterPorIdAsync(dto.AlunoId);

            if (aluno == null)
            {
                return BadRequest("Aluno não encontrado.");
            }

            var turma = await _turmaRepository.ObterPorIdAsync(dto.TurmaId);

            if (turma == null)
            {
                return BadRequest("Turma não encontrada.");
            }

            var nota = new Nota
            {
                Id = id,
                AlunoId = dto.AlunoId,
                TurmaId = dto.TurmaId,
                Valor = dto.Valor!.Value
            };

            var atualizado = await _notaRepository.AtualizarAsync(id, nota);

            if (!atualizado)
            {
                return NotFound("Nota não encontrada.");
            }

            return NoContent();
        }

        // DELETE: api/Notas/1
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteNota(int id)
        {
            var excluido = await _notaRepository.ExcluirAsync(id);

            if (!excluido)
            {
                return NotFound("Nota não encontrada.");
            }

            return NoContent();
        }
    }
}

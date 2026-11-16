using Microsoft.EntityFrameworkCore;
using SistemaGestaoAcademicaAPI.Data;
using SistemaGestaoAcademicaAPI.Models;
using SistemaGestaoAcademicaAPI.Repositories.Interfaces;

namespace SistemaGestaoAcademicaAPI.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly AppDbContext _context;

        public AlunoRepository(AppDbContext context)
        {
            _context = context;
        }

        // GET - Listar alunos
        public async Task<List<Aluno>> ObterTodosAsync()
        {
            return await _context.Alunos.ToListAsync();
        }

        // GET - Buscar aluno pelo ID
        public async Task<Aluno?> ObterPorIdAsync(int id)
        {
            return await _context.Alunos.FindAsync(id);
        }

        // POST - Cadastrar aluno
        public async Task<Aluno> AdicionarAsync(Aluno aluno)
        {
            _context.Alunos.Add(aluno);

            await _context.SaveChangesAsync();

            return aluno;
        }

        // PUT - Atualizar aluno
        public async Task<bool> AtualizarAsync(int id, Aluno aluno)
        {
            var alunoExistente = await _context.Alunos.FindAsync(id);

            if (alunoExistente == null)
            {
                return false;
            }

            alunoExistente.Nome = aluno.Nome;
            alunoExistente.Matricula = aluno.Matricula;
            alunoExistente.Curso = aluno.Curso;

            await _context.SaveChangesAsync();

            return true;
        }

        // DELETE - Excluir aluno
        public async Task<bool> ExcluirAsync(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);

            if (aluno == null)
            {
                return false;
            }

            _context.Alunos.Remove(aluno);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
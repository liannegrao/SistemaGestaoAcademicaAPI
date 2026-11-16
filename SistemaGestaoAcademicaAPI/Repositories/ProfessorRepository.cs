using Microsoft.EntityFrameworkCore;
using SistemaGestaoAcademicaAPI.Data;
using SistemaGestaoAcademicaAPI.Models;
using SistemaGestaoAcademicaAPI.Repositories.Interfaces;

namespace SistemaGestaoAcademicaAPI.Repositories
{
    public class ProfessorRepository : IProfessorRepository
    {
        private readonly AppDbContext _context;

        public ProfessorRepository(AppDbContext context)
        {
            _context = context;
        }

        // GET - Listar professores
        public async Task<List<Professor>> ObterTodosAsync()
        {
            return await _context.Professores.ToListAsync();
        }

        // GET - Buscar professor pelo ID
        public async Task<Professor?> ObterPorIdAsync(int id)
        {
            return await _context.Professores.FindAsync(id);
        }

        // POST - Cadastrar professor
        public async Task<Professor> AdicionarAsync(Professor professor)
        {
            _context.Professores.Add(professor);

            await _context.SaveChangesAsync();

            return professor;
        }

        // PUT - Atualizar professor
        public async Task<bool> AtualizarAsync(int id, Professor professor)
        {
            var professorExistente = await _context.Professores.FindAsync(id);

            if (professorExistente == null)
            {
                return false;
            }

            professorExistente.Nome = professor.Nome;
            professorExistente.Especialidade = professor.Especialidade;

            await _context.SaveChangesAsync();

            return true;
        }

        // DELETE - Excluir professor
        public async Task<bool> ExcluirAsync(int id)
        {
            var professor = await _context.Professores.FindAsync(id);

            if (professor == null)
            {
                return false;
            }

            _context.Professores.Remove(professor);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using SistemaGestaoAcademicaAPI.Data;
using SistemaGestaoAcademicaAPI.Models;
using SistemaGestaoAcademicaAPI.Repositories.Interfaces;

namespace SistemaGestaoAcademicaAPI.Repositories
{
    public class TurmaRepository : ITurmaRepository
    {
        private readonly AppDbContext _context;

        public TurmaRepository(AppDbContext context)
        {
            _context = context;
        }

        // GET - Listar todas as turmas com professor e disciplina
        public async Task<List<Turma>> ObterTodosAsync()
        {
            return await _context.Turmas
                .Include(t => t.Professor)
                .Include(t => t.Disciplina)
                .AsNoTracking()
                .ToListAsync();
        }

        // GET - Buscar turma pelo ID com professor e disciplina
        public async Task<Turma?> ObterPorIdAsync(int id)
        {
            return await _context.Turmas
                .Include(t => t.Professor)
                .Include(t => t.Disciplina)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        // POST - Cadastrar turma
        public async Task<Turma> AdicionarAsync(Turma turma)
        {
            _context.Turmas.Add(turma);

            await _context.SaveChangesAsync();

            return turma;
        }

        // PUT - Atualizar turma
        public async Task<bool> AtualizarAsync(int id, Turma turma)
        {
            var turmaExistente = await _context.Turmas.FindAsync(id);

            if (turmaExistente == null)
            {
                return false;
            }

            turmaExistente.Nome = turma.Nome;
            turmaExistente.ProfessorId = turma.ProfessorId;
            turmaExistente.DisciplinaId = turma.DisciplinaId;

            await _context.SaveChangesAsync();

            return true;
        }

        // DELETE - Excluir turma
        public async Task<bool> ExcluirAsync(int id)
        {
            var turma = await _context.Turmas.FindAsync(id);

            if (turma == null)
            {
                return false;
            }

            _context.Turmas.Remove(turma);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}

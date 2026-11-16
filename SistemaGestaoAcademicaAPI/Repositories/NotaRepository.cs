
using Microsoft.EntityFrameworkCore;
using SistemaGestaoAcademicaAPI.Data;
using SistemaGestaoAcademicaAPI.Models;
using SistemaGestaoAcademicaAPI.Repositories.Interfaces;

namespace SistemaGestaoAcademicaAPI.Repositories
{
    public class NotaRepository : INotaRepository
    {
        private readonly AppDbContext _context;

        public NotaRepository(AppDbContext context)
        {
            _context = context;
        }

        // GET - Listar todas as notas
        public async Task<List<Nota>> ObterTodosAsync()
        {
            return await _context.Notas
                .Include(n => n.Aluno)
                .Include(n => n.Turma)
                .AsNoTracking()
                .ToListAsync();
        }

        // GET - Buscar nota pelo ID
        public async Task<Nota?> ObterPorIdAsync(int id)
        {
            return await _context.Notas
                .Include(n => n.Aluno)
                .Include(n => n.Turma)
                .AsNoTracking()
                .FirstOrDefaultAsync(n => n.Id == id);
        }

        // GET - Buscar notas de um aluno
        public async Task<List<Nota>> ObterPorAlunoAsync(int alunoId)
        {
            return await _context.Notas
                .Where(n => n.AlunoId == alunoId)
                .Include(n => n.Aluno)
                .Include(n => n.Turma)
                .AsNoTracking()
                .ToListAsync();
        }

        // POST - Cadastrar nota
        public async Task<Nota> AdicionarAsync(Nota nota)
        {
            _context.Notas.Add(nota);

            await _context.SaveChangesAsync();

            return nota;
        }

        // PUT - Atualizar nota
        public async Task<bool> AtualizarAsync(int id, Nota nota)
        {
            var notaExistente = await _context.Notas.FindAsync(id);

            if (notaExistente == null)
            {
                return false;
            }

            notaExistente.AlunoId = nota.AlunoId;
            notaExistente.TurmaId = nota.TurmaId;
            notaExistente.Valor = nota.Valor;

            await _context.SaveChangesAsync();

            return true;
        }

        // DELETE - Excluir nota
        public async Task<bool> ExcluirAsync(int id)
        {
            var nota = await _context.Notas.FindAsync(id);

            if (nota == null)
            {
                return false;
            }

            _context.Notas.Remove(nota);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}

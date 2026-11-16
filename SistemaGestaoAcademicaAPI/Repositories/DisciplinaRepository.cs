using Microsoft.EntityFrameworkCore;
using SistemaGestaoAcademicaAPI.Data;
using SistemaGestaoAcademicaAPI.Models;
using SistemaGestaoAcademicaAPI.Repositories.Interfaces;

namespace SistemaGestaoAcademicaAPI.Repositories
{
    public class DisciplinaRepository : IDisciplinaRepository
    {
        private readonly AppDbContext _context;

        public DisciplinaRepository(AppDbContext context)
        {
            _context = context;
        }

        // GET - Listar disciplinas
        public async Task<List<Disciplina>> ObterTodosAsync()
        {
            return await _context.Disciplinas.ToListAsync();
        }

        // GET - Buscar disciplina pelo ID
        public async Task<Disciplina?> ObterPorIdAsync(int id)
        {
            return await _context.Disciplinas.FindAsync(id);
        }

        // POST - Cadastrar disciplina
        public async Task<Disciplina> AdicionarAsync(Disciplina disciplina)
        {
            _context.Disciplinas.Add(disciplina);

            await _context.SaveChangesAsync();

            return disciplina;
        }

        // PUT - Atualizar disciplina
        public async Task<bool> AtualizarAsync(int id, Disciplina disciplina)
        {
            var disciplinaExistente = await _context.Disciplinas.FindAsync(id);

            if (disciplinaExistente == null)
            {
                return false;
            }

            disciplinaExistente.Nome = disciplina.Nome;
            disciplinaExistente.CargaHoraria = disciplina.CargaHoraria;

            await _context.SaveChangesAsync();

            return true;
        }

        // DELETE - Excluir disciplina
        public async Task<bool> ExcluirAsync(int id)
        {
            var disciplina = await _context.Disciplinas.FindAsync(id);

            if (disciplina == null)
            {
                return false;
            }

            _context.Disciplinas.Remove(disciplina);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
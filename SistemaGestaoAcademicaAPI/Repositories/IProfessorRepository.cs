using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaGestaoAcademicaAPI.Models;

namespace SistemaGestaoAcademicaAPI.Repositories.Interfaces
{
    public interface IProfessorRepository
    {
        // Listar todos os professores
        Task<List<Professor>> ObterTodosAsync();

        // Buscar professor pelo ID
        Task<Professor?> ObterPorIdAsync(int id);

        // Cadastrar professor
        Task<Professor> AdicionarAsync(Professor professor);

        // Atualizar professor
        Task<bool> AtualizarAsync(int id, Professor professor);

        // Excluir professor
        Task<bool> ExcluirAsync(int id);
    }
}
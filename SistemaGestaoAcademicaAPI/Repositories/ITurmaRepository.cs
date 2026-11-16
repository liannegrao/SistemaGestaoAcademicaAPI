using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaGestaoAcademicaAPI.Models;

namespace SistemaGestaoAcademicaAPI.Repositories.Interfaces
{
    public interface ITurmaRepository
    {
        // Listar todas as turmas
        Task<List<Turma>> ObterTodosAsync();

        // Buscar turma pelo ID
        Task<Turma?> ObterPorIdAsync(int id);

        // Cadastrar turma
        Task<Turma> AdicionarAsync(Turma turma);

        // Atualizar turma
        Task<bool> AtualizarAsync(int id, Turma turma);

        // Excluir turma
        Task<bool> ExcluirAsync(int id);
    }
}
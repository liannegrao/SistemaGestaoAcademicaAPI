using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaGestaoAcademicaAPI.Models;

namespace SistemaGestaoAcademicaAPI.Repositories.Interfaces
{
    public interface IDisciplinaRepository
    {
        // Listar todas as disciplinas
        Task<List<Disciplina>> ObterTodosAsync();

        // Buscar disciplina pelo ID
        Task<Disciplina?> ObterPorIdAsync(int id);

        // Cadastrar disciplina
        Task<Disciplina> AdicionarAsync(Disciplina disciplina);

        // Atualizar disciplina
        Task<bool> AtualizarAsync(int id, Disciplina disciplina);

        // Excluir disciplina
        Task<bool> ExcluirAsync(int id);
    }
}
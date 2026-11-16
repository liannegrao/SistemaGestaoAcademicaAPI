
using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaGestaoAcademicaAPI.Models;

namespace SistemaGestaoAcademicaAPI.Repositories.Interfaces
{
    public interface INotaRepository
    {
        // Listar todas as notas
        Task<List<Nota>> ObterTodosAsync();

        // Buscar nota pelo ID
        Task<Nota?> ObterPorIdAsync(int id);

        // Buscar todas as notas de um aluno
        Task<List<Nota>> ObterPorAlunoAsync(int alunoId);

        // Cadastrar nota
        Task<Nota> AdicionarAsync(Nota nota);

        // Atualizar nota
        Task<bool> AtualizarAsync(int id, Nota nota);

        // Excluir nota
        Task<bool> ExcluirAsync(int id);
    }
}

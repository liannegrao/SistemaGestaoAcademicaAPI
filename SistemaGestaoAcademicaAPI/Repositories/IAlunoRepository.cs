using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaGestaoAcademicaAPI.Models;

namespace SistemaGestaoAcademicaAPI.Repositories.Interfaces
{
    public interface IAlunoRepository
    {
        // Listar todos os alunos
        Task<List<Aluno>> ObterTodosAsync();

        // Buscar aluno pelo ID
        Task<Aluno?> ObterPorIdAsync(int id);

        // Cadastrar aluno
        Task<Aluno> AdicionarAsync(Aluno aluno);

        // Atualizar aluno
        Task<bool> AtualizarAsync(int id, Aluno aluno);

        // Excluir aluno
        Task<bool> ExcluirAsync(int id);
    }
}
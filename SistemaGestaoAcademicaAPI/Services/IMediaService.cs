using System.Threading.Tasks;

namespace SistemaGestaoAcademicaAPI.Services
{
    public interface IMediaService
    {
        // Calcular a média e a situação do aluno
        Task<ResultadoMediaAluno> CalcularMediaAlunoAsync(int alunoId);
    }
}
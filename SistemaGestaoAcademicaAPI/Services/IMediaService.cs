using System.Linq;
using SistemaGestaoAcademicaAPI.Repositories.Interfaces;

namespace SistemaGestaoAcademicaAPI.Services
{
    public class MediaService
    {
        private readonly INotaRepository _notaRepository;

        // Média mínima para aprovação
        private const decimal MediaMinima = 7.0m;

        public MediaService(INotaRepository notaRepository)
        {
            _notaRepository = notaRepository;
        }

        // Calcular a média das notas do aluno
        public async Task<ResultadoMediaAluno> CalcularMediaAlunoAsync(int alunoId)
        {
            var notas = await _notaRepository.ObterPorAlunoAsync(alunoId);

            // Verificar se o aluno possui notas
            if (notas.Count == 0)
            {
                return new ResultadoMediaAluno
                {
                    AlunoId = alunoId,
                    QuantidadeNotas = 0,
                    Media = null,
                    Situacao = "Sem notas"
                };
            }

            // Calcular a média aritmética
            decimal media = notas.Average(n => n.Valor);

            // Arredondar para duas casas decimais
            media = Math.Round(media, 2);

            // Verificar aprovação
            string situacao = media >= MediaMinima
                ? "Aprovado"
                : "Reprovado";

            return new ResultadoMediaAluno
            {
                AlunoId = alunoId,
                QuantidadeNotas = notas.Count,
                Media = media,
                Situacao = situacao
            };
        }
    }

    // Resultado do cálculo
    public class ResultadoMediaAluno
    {
        public int AlunoId { get; set; }

        public int QuantidadeNotas { get; set; }

        public decimal? Media { get; set; }

        public string Situacao { get; set; } = string.Empty;
    }
}

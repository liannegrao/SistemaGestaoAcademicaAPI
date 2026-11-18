using System;
using System.Linq;
using System.Threading.Tasks;
using SistemaGestaoAcademicaAPI.Repositories.Interfaces;

namespace SistemaGestaoAcademicaAPI.Services
{
    public class MediaService : IMediaService
    {
        private readonly INotaRepository _notaRepository;

        // Média mínima para aprovação
        private const decimal MediaMinima = 7.0m;

        // Construtor
        public MediaService(INotaRepository notaRepository)
        {
            _notaRepository = notaRepository;
        }

        // Calcular a média das notas do aluno
        public async Task<ResultadoMediaAluno> CalcularMediaAlunoAsync(int alunoId)
        {
            // Buscar todas as notas do aluno no MySQL
            var notas = await _notaRepository.ObterPorAlunoAsync(alunoId);

            // Verificar se o aluno possui notas cadastradas
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

            // Verificar aprovação antes do arredondamento
            string situacao = media >= MediaMinima
                ? "Aprovado"
                : "Reprovado";

            // Arredondar a média para duas casas decimais
            media = Math.Round(media, 2, MidpointRounding.AwayFromZero);

            // Retornar o resultado
            return new ResultadoMediaAluno
            {
                AlunoId = alunoId,
                QuantidadeNotas = notas.Count,
                Media = media,
                Situacao = situacao
            };
        }
    }

    // Classe que representa o resultado da média
    public class ResultadoMediaAluno
    {
        public int AlunoId { get; set; }

        public int QuantidadeNotas { get; set; }

        public decimal? Media { get; set; }

        public string Situacao { get; set; } = string.Empty;
    }
}
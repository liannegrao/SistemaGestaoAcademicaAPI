
using System.ComponentModel.DataAnnotations;

namespace SistemaGestaoAcademicaAPI.DTOs
{
    public class NotaDTO
    {
        // ID do aluno que receberá a nota
        [Range(1, int.MaxValue,
            ErrorMessage = "Informe um ID de aluno válido.")]
        public int AlunoId { get; set; }

        // ID da turma
        [Range(1, int.MaxValue,
            ErrorMessage = "Informe um ID de turma válido.")]
        public int TurmaId { get; set; }

        // Valor da nota: entre 0 e 10
        [Required(ErrorMessage = "Informe o valor da nota.")]
        [Range(typeof(decimal), "0", "10",
            ErrorMessage = "A nota deve estar entre 0 e 10.")]
        public decimal? Valor { get; set; }
    }
}

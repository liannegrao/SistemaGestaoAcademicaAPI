
using System.ComponentModel.DataAnnotations;

namespace SistemaGestaoAcademicaAPI.Models
{
    public class Nota
    {
        public int Id { get; set; }

        // Relacionamento com Aluno
        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; } = null!;

        // Relacionamento com Turma
        public int TurmaId { get; set; }
        public Turma Turma { get; set; } = null!;

        // Valor da nota
        [Range(typeof(decimal), "0", "10",
            ErrorMessage = "A nota deve estar entre 0 e 10.")]
        public decimal Valor { get; set; }
    }
}

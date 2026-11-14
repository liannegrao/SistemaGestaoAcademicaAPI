
using System.ComponentModel.DataAnnotations;

namespace SistemaGestaoAcademicaAPI.Models
{
    public class Turma
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da turma é obrigatório.")]
        [MaxLength(80)]
        public string Nome { get; set; } = string.Empty;

        // Relacionamento com Professor
        public int ProfessorId { get; set; }
        public Professor Professor { get; set; } = null!;

        // Relacionamento com Disciplina
        public int DisciplinaId { get; set; }
        public Disciplina Disciplina { get; set; } = null!;
    }
}

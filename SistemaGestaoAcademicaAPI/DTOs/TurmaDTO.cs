
using System.ComponentModel.DataAnnotations;

namespace SistemaGestaoAcademicaAPI.DTOs
{
    public class TurmaDTO
    {
        [Required]
        public string Nome { get; set; } = string.Empty;

        [Range(1, int.MaxValue)]
        public int ProfessorId { get; set; }

        [Range(1, int.MaxValue)]
        public int DisciplinaId { get; set; }
    }
}

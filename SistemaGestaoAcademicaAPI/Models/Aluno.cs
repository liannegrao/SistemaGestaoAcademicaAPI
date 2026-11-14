using System.ComponentModel.DataAnnotations;

namespace SistemaGestaoAcademicaAPI.Models
{
    public class Aluno
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MaxLength(80)]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "A matrícula é obrigatória.")]
        [MaxLength(20)]
        public string Matricula { get; set; } = string.Empty;

        [Required(ErrorMessage = "O curso é obrigatório.")]
        [MaxLength(100)]
        public string Curso { get; set; } = string.Empty;
    }
}
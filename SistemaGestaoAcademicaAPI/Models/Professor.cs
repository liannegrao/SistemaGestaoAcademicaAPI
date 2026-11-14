using System.ComponentModel.DataAnnotations;

namespace SistemaGestaoAcademicaAPI.Models
{
    public class Professor
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(80)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Especialidade { get; set; } = string.Empty;
    }
}

using System.ComponentModel.DataAnnotations;

namespace SistemaGestaoAcademicaAPI.Models
{
    public class Disciplina
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da disciplina é obrigatório.")]
        [MaxLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Range(1, 1000, ErrorMessage = "A carga horária deve ser maior que zero.")]
        public int CargaHoraria { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace SistemaGestaoAcademicaAPI.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(120)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string SenhaHash { get; set; } = string.Empty;
    }
}
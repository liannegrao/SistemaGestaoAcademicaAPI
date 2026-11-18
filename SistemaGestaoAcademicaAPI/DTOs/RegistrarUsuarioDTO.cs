using System.ComponentModel.DataAnnotations;

namespace SistemaGestaoAcademicaAPI.DTOs
{
    public class RegistrarUsuarioDTO
    {
        [Required]
        [StringLength(120, MinimumLength = 2)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(8)]
        public string Senha { get; set; } = string.Empty;
    }
}
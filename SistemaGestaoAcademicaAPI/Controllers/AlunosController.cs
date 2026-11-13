using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SistemaGestaoAcademicaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAlunos()
        {
            var alunos = new[]
            {
                new { Id = 1, Nome = "Ana", Matricula = "2026001" },
                new { Id = 2, Nome = "Carlos", Matricula = "2026002" },
                new { Id = 3, Nome = "Maria", Matricula = "2026003" }
            };

            return Ok(alunos);
        }
    }
}

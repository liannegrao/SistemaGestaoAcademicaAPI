using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SistemaGestaoAcademicaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SegurancaController : ControllerBase
    {
        // GET: api/Seguranca/teste
        [Authorize]
        [HttpGet("teste")]
        public IActionResult TestarAutorizacao()
        {
            return Ok(new
            {
                Mensagem = "Token JWT validado com sucesso!",
                Situacao = "Acesso autorizado"
            });
        }
    }
}
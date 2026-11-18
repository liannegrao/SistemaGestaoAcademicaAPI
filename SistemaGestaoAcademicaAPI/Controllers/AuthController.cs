using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using SistemaGestaoAcademicaAPI.Data;
using SistemaGestaoAcademicaAPI.DTOs;
using SistemaGestaoAcademicaAPI.Models;

namespace SistemaGestaoAcademicaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher<Usuario> _passwordHasher;

        public AuthController(
            AppDbContext context,
            IConfiguration configuration,
            IPasswordHasher<Usuario> passwordHasher)
        {
            _context = context;
            _configuration = configuration;
            _passwordHasher = passwordHasher;
        }

        // =====================================
        // CADASTRAR USUÁRIO
        // POST: api/Auth/registrar
        // =====================================

        [HttpPost("registrar")]
        [AllowAnonymous]
        public async Task<IActionResult> Registrar(
            [FromBody] RegistrarUsuarioDTO dto)
        {
            string email = dto.Email.Trim().ToLowerInvariant();

            // Verificar se o e-mail já está cadastrado
            bool emailExiste = await _context.Usuarios
                .AnyAsync(u => u.Email == email);

            if (emailExiste)
            {
                return Conflict(new
                {
                    mensagem = "Este e-mail já está cadastrado."
                });
            }

            // Criar o usuário
            var usuario = new Usuario
            {
                Nome = dto.Nome.Trim(),
                Email = email,
                SenhaHash = string.Empty
            };

            // Proteger a senha com hash
            usuario.SenhaHash = _passwordHasher
                .HashPassword(usuario, dto.Senha);

            // Salvar no MySQL
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            // Retornar os dados sem mostrar a senha
            return StatusCode(201, new
            {
                mensagem = "Usuário cadastrado com sucesso!",
                usuario.Id,
                usuario.Nome,
                usuario.Email
            });
        }

        // =====================================
        // LOGIN DO USUÁRIO
        // POST: api/Auth/login
        // =====================================

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(
            [FromBody] LoginDTO dto)
        {
            string email = dto.Email.Trim().ToLowerInvariant();

            // Buscar o usuário pelo e-mail
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email);

            if (usuario == null)
            {
                return Unauthorized(new
                {
                    mensagem = "E-mail ou senha inválidos."
                });
            }

            // Verificar a senha informada
            var resultadoSenha = _passwordHasher
                .VerifyHashedPassword(
                    usuario,
                    usuario.SenhaHash,
                    dto.Senha
                );

            if (resultadoSenha == PasswordVerificationResult.Failed)
            {
                return Unauthorized(new
                {
                    mensagem = "E-mail ou senha inválidos."
                });
            }

            // Buscar a chave JWT dos Segredos do Usuário
            string chaveBase64 = _configuration["Jwt:Key"]
                ?? throw new InvalidOperationException(
                    "A chave JWT não foi configurada.");

            byte[] chaveJwt = Convert.FromBase64String(chaveBase64);

            // Configurar a assinatura do token
            var chaveSeguranca = new SymmetricSecurityKey(chaveJwt);

            var credenciais = new SigningCredentials(
                chaveSeguranca,
                SecurityAlgorithms.HmacSha256
            );

            // Definir a expiração do token
            DateTime expiracao = DateTime.UtcNow.AddHours(2);

            // Informações do usuário incluídas no token
            var claims = new[]
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    usuario.Id.ToString()),

                new Claim(
                    ClaimTypes.Name,
                    usuario.Nome),

                new Claim(
                    ClaimTypes.Email,
                    usuario.Email),

                new Claim(
                    "jti",
                    Guid.NewGuid().ToString())
            };

            // Criar o JWT
            var descricaoToken = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),

                Issuer = _configuration["Jwt:Issuer"],

                Audience = _configuration["Jwt:Audience"],

                Expires = expiracao,

                SigningCredentials = credenciais
            };

            // Converter o token para texto
            var geradorToken = new JsonWebTokenHandler();

            string token = geradorToken.CreateToken(descricaoToken);

            // Retornar o JWT
            return Ok(new
            {
                mensagem = "Login realizado com sucesso!",
                token,
                tipo = "Bearer",
                expiracaoUtc = expiracao
            });
        }
    }
}
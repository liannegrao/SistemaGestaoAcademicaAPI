using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Microsoft.AspNetCore.Identity;
using SistemaGestaoAcademicaAPI.Models;
using SistemaGestaoAcademicaAPI.Data;
using SistemaGestaoAcademicaAPI.Repositories;
using SistemaGestaoAcademicaAPI.Repositories.Interfaces;
using SistemaGestaoAcademicaAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// =====================================
// REGISTRAR OS CONTROLLERS
// =====================================

builder.Services.AddControllers();

// =====================================
// CONFIGURAÇÃO DO SWAGGER COM JWT
// =====================================

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    // Configurar o botão Authorize no Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "Insira seu token JWT para acessar os endpoints protegidos."
    });

    // Informar que a API utiliza autenticação JWT
    options.AddSecurityRequirement(document =>
        new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference("Bearer", document)] = []
        });
});

// =====================================
// CONFIGURAÇÃO DO MYSQL
// =====================================

// Obter a conexão com o MySQL
var connectionString = builder.Configuration
    .GetConnectionString("ConexaoBanco")
    ?? throw new InvalidOperationException(
        "A conexão ConexaoBanco não foi configurada.");

// Registrar o Entity Framework Core com MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString)
    )
);

// =====================================
// REGISTRAR OS REPOSITORIES
// =====================================

builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();

builder.Services.AddScoped<IProfessorRepository, ProfessorRepository>();

builder.Services.AddScoped<IDisciplinaRepository, DisciplinaRepository>();

builder.Services.AddScoped<ITurmaRepository, TurmaRepository>();

builder.Services.AddScoped<INotaRepository, NotaRepository>();

// =====================================
// REGISTRAR OS SERVICES
// =====================================

// Serviço para calcular a média dos alunos
builder.Services.AddScoped<IMediaService, MediaService>();

// Serviço para proteger e verificar as senhas dos usuários
builder.Services.AddScoped<
    IPasswordHasher<Usuario>,
    PasswordHasher<Usuario>>();

// =====================================
// CONFIGURAÇÃO DO JWT
// =====================================

// Buscar a chave JWT nos Segredos do Usuário
var chaveBase64 = builder.Configuration["Jwt:Key"]
    ?? throw new InvalidOperationException(
        "A chave JWT não foi configurada.");

// Converter a chave Base64 para bytes
var chaveJwt = Convert.FromBase64String(chaveBase64);

// Verificar o tamanho da chave
if (chaveJwt.Length < 32)
{
    throw new InvalidOperationException(
        "A chave JWT precisa ter pelo menos 32 bytes.");
}

// Configurar a autenticação JWT
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // Definir as regras de validação do token
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // Verificar se o token foi assinado corretamente
            ValidateIssuerSigningKey = true,

            IssuerSigningKey = new SymmetricSecurityKey(chaveJwt),

            // Verificar quem criou o token
            ValidateIssuer = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],

            // Verificar para quem o token foi criado
            ValidateAudience = true,

            ValidAudience = builder.Configuration["Jwt:Audience"],

            // Verificar se o token expirou
            ValidateLifetime = true,

            // Exigir que o token tenha data de expiração
            RequireExpirationTime = true,

            // Tolerância de um minuto na validação
            ClockSkew = TimeSpan.FromMinutes(1)
        };
    });

// =====================================
// REGISTRAR A AUTORIZAÇÃO
// =====================================

builder.Services.AddAuthorization();

// =====================================
// CONSTRUIR A APLICAÇÃO
// =====================================

var app = builder.Build();

// =====================================
// CONFIGURAÇÃO DO SWAGGER
// =====================================

// Habilitar o Swagger em desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

// =====================================
// CONFIGURAÇÃO DO HTTPS
// =====================================

app.UseHttpsRedirection();

// =====================================
// AUTENTICAÇÃO E AUTORIZAÇÃO
// =====================================

// Verificar a identidade do usuário
app.UseAuthentication();

// Verificar as permissões do usuário
app.UseAuthorization();

// =====================================
// HABILITAR OS CONTROLLERS
// =====================================

app.MapControllers();

// =====================================
// EXECUTAR A APLICAÇÃO
// =====================================

app.Run();
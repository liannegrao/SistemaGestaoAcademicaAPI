
using Microsoft.EntityFrameworkCore;
using SistemaGestaoAcademicaAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Registrar os Controllers
builder.Services.AddControllers();

// Registrar o Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

var app = builder.Build();

// Habilitar a interface do Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

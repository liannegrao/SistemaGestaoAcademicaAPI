using Microsoft.EntityFrameworkCore;
using SistemaGestaoAcademicaAPI.Models;

namespace SistemaGestaoAcademicaAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Tabela de alunos
        public DbSet<Aluno> Alunos { get; set; } = null!;

        // Tabela de professores
        public DbSet<Professor> Professores { get; set; } = null!;

        // Tabela de disciplinas
        public DbSet<Disciplina> Disciplinas { get; set; } = null!;

        // Tabela de turmas
        public DbSet<Turma> Turmas { get; set; } = null!;

        // Tabela de notas
        public DbSet<Nota> Notas { get; set; } = null!;

        // Tabela de usuários
        public DbSet<Usuario> Usuarios { get; set; } = null!;

        // =====================================
        // CONFIGURAÇÕES DO BANCO DE DADOS
        // =====================================

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Impedir que dois usuários tenham o mesmo e-mail
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
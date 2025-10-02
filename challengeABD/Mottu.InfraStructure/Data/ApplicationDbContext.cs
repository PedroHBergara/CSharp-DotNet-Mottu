using Microsoft.EntityFrameworkCore;
using Mottu.Domain.Entities;

namespace Mottu.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Moto> Motos { get; set; }
        public DbSet<Motorista> Motoristas { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Moto entity
            modelBuilder.Entity<Moto>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Modelo).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Placa).IsRequired().HasMaxLength(7);
            });

            // Configure Motorista entity
            modelBuilder.Entity<Motorista>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired();
                entity.Property(e => e.DataNascimento).IsRequired();
                entity.HasOne(m => m.UltimaMotoUsada)
                      .WithMany()
                      .HasForeignKey(m => m.UltimaMotoUsadaId)
                      .IsRequired(false); // UltimaMotoUsadaId is nullable
            });

            // Configure Funcionario entity
            modelBuilder.Entity<Funcionario>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired();
                entity.Property(e => e.DataNascimento).IsRequired();
                entity.Property(e => e.Funcao).IsRequired();
            });
        }
    }
}

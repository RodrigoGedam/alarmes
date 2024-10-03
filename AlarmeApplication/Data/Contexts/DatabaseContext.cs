using AlarmeApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace AlarmeApplication.Data.Contexts
{
    public class DatabaseContext : DbContext
    {
        public virtual DbSet<OcorrenciaModel> Ocorrencias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OcorrenciaModel>(entity =>
            {
                entity.ToTable("Ocorrencias");
                entity.HasKey(e => e.OcorrenciaId);
                entity.Property(e => e.Prioridade).IsRequired();
                entity.Property(e => e.Localizacao).IsRequired();
                entity.Property(e => e.Descricao).IsRequired();
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.Data).HasColumnType("date").IsRequired();
            });
        }

        public DatabaseContext(DbContextOptions options) : base(options) { }

        protected DatabaseContext() { }
    }
}

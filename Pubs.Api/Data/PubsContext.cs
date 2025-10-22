// Data/PubsContext.cs
using Microsoft.EntityFrameworkCore;
using Pubs.Api.Models;

namespace Pubs.API.Data
{
    public class PubsContext(DbContextOptions<PubsContext> options) : DbContext(options)
    {

        // Apenas as 3 entidades que foram definidas
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<Sale> Sales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar chave composta para Sales
            modelBuilder.Entity<Sale>()
                .HasKey(s => new { s.StorId, s.OrdNum });

            // Configurar relacionamentos
            modelBuilder.Entity<Publisher>()
                .HasMany(p => p.Titles)
                .WithOne(t => t.Publisher)
                .HasForeignKey(t => t.PubId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Title>()
                .HasMany(t => t.Sales)
                .WithOne(s => s.Title)
                .HasForeignKey(s => s.TitleId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configurar tamanhos de colunas para SQL Server
            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.HasKey(e => e.PubId);
                entity.Property(e => e.PubId)
                    .HasMaxLength(4)
                    .IsRequired();
                entity.Property(e => e.PubName)
                    .HasMaxLength(40);
                entity.Property(e => e.City)
                    .HasMaxLength(20);
                entity.Property(e => e.State)
                    .HasMaxLength(2);
                entity.Property(e => e.Country)
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Title>(entity =>
            {
                entity.HasKey(e => e.TitleId);
                entity.Property(e => e.TitleId)
                    .HasMaxLength(6)
                    .IsRequired();
                entity.Property(e => e.TitleName)
                    .HasMaxLength(80)
                    .IsRequired();
                entity.Property(e => e.Type)
                    .HasMaxLength(12)
                    .HasDefaultValue("UNDECIDED");
                entity.Property(e => e.PubId)
                    .HasMaxLength(4);
                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18,2)");
                entity.Property(e => e.Advance)
                    .HasColumnType("decimal(18,2)");
                entity.Property(e => e.Notes)
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasKey(e => new { e.StorId, e.OrdNum });
                entity.Property(e => e.StorId)
                    .HasMaxLength(4)
                    .IsRequired();
                entity.Property(e => e.OrdNum)
                    .HasMaxLength(20)
                    .IsRequired();
                entity.Property(e => e.Payterms)
                    .HasMaxLength(12);
                entity.Property(e => e.TitleId)
                    .HasMaxLength(6)
                    .IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
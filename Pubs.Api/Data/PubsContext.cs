using Microsoft.EntityFrameworkCore;
using Pubs.Api.Models;

namespace Pubs.Api.Data
{
    public class PubsContext : DbContext
    {
        public PubsContext(DbContextOptions<PubsContext> options) : base(options) { }

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
        }
    }
}

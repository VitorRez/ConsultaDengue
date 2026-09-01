using Microsoft.EntityFrameworkCore;
using ConsultaDengue.Models;

namespace ConsultaDengue.Data{
    public class AppDbContext : DbContext{
        public DbSet<DengueAlert> DengueAlerts {get;set;}

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DengueAlert>().ToTable("DengueAlerts");

            modelBuilder.Entity<DengueAlert>()
                .HasIndex(d => new {d.Geocode, d.AnoEpidemiologico, d.SemanaEpidemiologica})
                .IsUnique();

            modelBuilder.Entity<DengueAlert>().ToTable("DengueAlerts");
        }
    }
}
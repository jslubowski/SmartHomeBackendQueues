using Microsoft.EntityFrameworkCore;
using SmartHome.BLL.Entities;
using SmartHome.DAL.Configuration;
using SmartHome.DAL.Configuration.Enums;

namespace SmartHome.DAL.Data
{
    public class PostgresDbContext : DbContext
    {
        public DbSet<Temperature> Temperatures { get; set; }

        public PostgresDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TemperatureConfiguration());
            modelBuilder.ApplyConfiguration(new TemperatureVariantConfiguration());
        }
    }
}

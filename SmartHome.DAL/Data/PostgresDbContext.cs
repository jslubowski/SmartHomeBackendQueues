using Microsoft.EntityFrameworkCore;
using SmartHome.BLL.Entities;
using SmartHome.DAL.Configuration;
using SmartHome.DAL.Configuration.Enums;

namespace SmartHome.DAL.Data
{
    public class PostgresDbContext : DbContext
    {
        //public DbSet<Measurement> Measurements { get; set; }
        public DbSet<Sensor> Sensors { get; set; }

        public PostgresDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MeasurementConfiguration());
            modelBuilder.ApplyConfiguration(new SensorConfiguration());
            modelBuilder.ApplyConfiguration(new MeasurementTypeVariantConfiguration());
            modelBuilder.ApplyConfiguration(new MeasurementUnitVariantConfiguration());
        }
    }
}

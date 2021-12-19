using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHome.BLL.Entities;

namespace SmartHome.DAL.Configuration
{
    public class MeasurementConfiguration : IEntityTypeConfiguration<Measurement>
    {
        public void Configure(EntityTypeBuilder<Measurement> builder)
        {
            builder.HasKey(entity => entity.Id);
            builder
                .HasOne(measurement => measurement.Sensor)
                .WithMany(sensor => sensor.Measurements)
                .HasForeignKey(measurement => measurement.SensorId);
        }
    }
}

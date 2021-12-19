using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHome.BLL.Entities;

namespace SmartHome.DAL.Configuration
{
    public class SensorConfiguration : IEntityTypeConfiguration<Sensor>
    {
        public void Configure(EntityTypeBuilder<Sensor> builder)
        {
            builder.HasKey(entity => entity.Id);
            builder
                .Property(entity => entity.MeasurementType)
                .HasConversion<int>();
            builder
                .Property(entity => entity.MeasurementUnit)
                .HasConversion<int>();
        }
    }
}

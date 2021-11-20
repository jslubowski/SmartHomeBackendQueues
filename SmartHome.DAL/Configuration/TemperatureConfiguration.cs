using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHome.BLL.Entities;

namespace SmartHome.DAL.Configuration
{
    public class TemperatureConfiguration : IEntityTypeConfiguration<Temperature>
    {
        public void Configure(EntityTypeBuilder<Temperature> builder)
        {
            builder.HasKey(entity => entity.Id);
            builder
                .Property(entity => entity.TemperatureUnit)
                .HasConversion<int>();
        }
    }
}

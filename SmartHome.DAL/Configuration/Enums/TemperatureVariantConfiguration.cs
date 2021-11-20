using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHome.BLL.Entities.Enums;
using SmartHome.BLL.Enums;
using System;
using System.Linq;

namespace SmartHome.DAL.Configuration.Enums
{
    public class TemperatureVariantConfiguration : IEntityTypeConfiguration<TemperatureVariant>
    {
        public void Configure(EntityTypeBuilder<TemperatureVariant> builder)
        {
            builder.HasKey(entity => entity.TemperatureUnit);
            builder
                .Property(entity => entity.TemperatureUnit)
                .HasConversion<int>();
            builder
                .HasData(
                    Enum.GetValues(typeof(TemperatureUnit))
                    .Cast<TemperatureUnit>()
                    .Select(e => new TemperatureVariant()
                    {
                        TemperatureUnit = e,
                        Name = e.ToString()
                    })
                    );
        }
    }
}

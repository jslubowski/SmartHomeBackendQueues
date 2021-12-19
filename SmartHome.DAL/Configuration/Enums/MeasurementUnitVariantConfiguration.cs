using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHome.BLL.Entities.Enums;
using SmartHome.BLL.Enums;
using System;
using System.Linq;

namespace SmartHome.DAL.Configuration.Enums
{
    public class MeasurementUnitVariantConfiguration : IEntityTypeConfiguration<MeasurementUnitVariant>
    {
        public void Configure(EntityTypeBuilder<MeasurementUnitVariant> builder)
        {
            builder.HasKey(entity => entity.MeasurementUnit);
            builder
                .Property(entity => entity.MeasurementUnit)
                .HasConversion<int>();
            builder
                .HasData(
                    Enum.GetValues(typeof(MeasurementUnit))
                    .Cast<MeasurementUnit>()
                    .Select(e => new MeasurementUnitVariant()
                    {
                        MeasurementUnit = e,
                        Name = e.ToString()
                    })
                    );
        }
    }
}

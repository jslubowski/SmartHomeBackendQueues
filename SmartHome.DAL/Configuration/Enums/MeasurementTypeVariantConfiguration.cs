using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHome.BLL.Entities.Enums;
using SmartHome.BLL.Enums;
using System;
using System.Linq;

namespace SmartHome.DAL.Configuration.Enums
{
    public class MeasurementTypeVariantConfiguration : IEntityTypeConfiguration<MeasurementTypeVariant>
    {
        public void Configure(EntityTypeBuilder<MeasurementTypeVariant> builder)
        {
            builder.HasKey(entity => entity.MeasurementType);
            builder
                .Property(entity => entity.MeasurementType)
                .HasConversion<int>();
            builder
                .HasData(
                    Enum.GetValues(typeof(MeasurementType))
                    .Cast<MeasurementType>()
                    .Select(e => new MeasurementTypeVariant()
                    {
                        MeasurementType = e,
                        Name = e.ToString()
                    })
                    );
        }
    }
}

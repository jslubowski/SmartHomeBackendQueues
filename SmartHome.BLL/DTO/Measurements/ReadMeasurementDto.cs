using SmartHome.BLL.Enums;
using System;

namespace SmartHome.BLL.DTO.Measurements
{
    public class ReadMeasurementDto : GenericEventDto
    {
        public Guid SensorId { get; set; }
        public int MeasurementType { get; set; }
        public int MeasurementUnit { get; set; }
        public float Value { get; set; }
    }
}

using SmartHome.BLL.Enums;
using System;

namespace SmartHome.BLL.DTO.Measurements
{
    public class ReadMeasurementDto : GenericEventDto
    {
        public DateTime Date { get; set; }
        public Guid SensorId { get; set; }
        public int MeasurementType { get; set; }
        public int MeasurementUnit { get; set; }
        public float Value { get; set; }
    }
}

using System;

namespace SmartHome.BLL.DTO.Temperature
{
    public class AddMeasurementDto
    {
        public DateTime Date { get; set; }
        public Guid SensorId { get; set; }
        public int MeasurementType { get; set; }
        public int MeasurementUnit { get; set; }
        public float Value { get; set; }
    }
}

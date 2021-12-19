using System;

namespace SmartHome.BLL.DTO.Sensor
{
    public class AddSensorDto
    {
        public Guid Id { get; set; }
        public int MeasurementType { get; set; }
        public int MeasurementUnit { get; set; }
    }
}

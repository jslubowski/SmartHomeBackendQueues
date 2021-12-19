using SmartHome.BLL.Enums;
using System;

namespace SmartHome.BLL.DTO.Sensor
{
    public class SensorDto
    {
        public Guid Id { get; set; }
        public MeasurementType MeasurementType { get; set; }
        public MeasurementUnit MeasurementUnit { get; set; }
        public string CustomName { get; set; }

        public SensorDto(Entities.Sensor sensor)
        {
            Id = sensor.Id;
            MeasurementType = sensor.MeasurementType;
            MeasurementUnit = sensor.MeasurementUnit;
            CustomName = sensor.CustomName ?? string.Empty;
        }
    }
}

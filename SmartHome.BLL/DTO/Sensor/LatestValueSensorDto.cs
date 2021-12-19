using SmartHome.BLL.Enums;
using System;

namespace SmartHome.BLL.DTO.Sensor
{
    public class LatestValueSensorDto
    {
        public Guid Id { get; set; }
        public MeasurementType MeasurementType { get; set; }
        public MeasurementUnit MeasurementUnit { get; set; }
        public float LatestValue { get; set; }

        public LatestValueSensorDto(Entities.Sensor sensor)
        {
            Id = sensor.Id;
            MeasurementType = sensor.MeasurementType;
            MeasurementUnit = sensor.MeasurementUnit;
            LatestValue = sensor.LatestValue;
        }
    }
}

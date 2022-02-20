using SmartHome.BLL.DTO.Measurements;
using System;

namespace SmartHome.BLL.Entities
{
    public class Measurement
    {
        public long Id { get; protected set; }
        public DateTime Date { get; protected set; }
        public Guid SensorId { get; protected set; }
        // TODO convert to ValueObject
        public float Value { get; protected set; }

        public virtual Sensor Sensor { get; protected set; }

        public Measurement() { }

        public Measurement(ReadMeasurementDto addMeasurementDto)
        {
            Date = DateTime.Now;
            SensorId = addMeasurementDto.SensorId;
            Value = addMeasurementDto.Value;
        }
    }
}

﻿using SmartHome.BLL.DTO.Temperature;
using SmartHome.BLL.Enums;
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

        public Measurement(AddMeasurementDto addMeasurementDto)
        {
            Date = addMeasurementDto.Date;
            SensorId = addMeasurementDto.SensorId;
            Value = addMeasurementDto.Value;
        }
    }
}
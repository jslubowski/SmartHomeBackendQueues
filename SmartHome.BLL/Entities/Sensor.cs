using SmartHome.BLL.DTO;
using SmartHome.BLL.DTO.Measurements;
using SmartHome.BLL.DTO.Sensor;
using SmartHome.BLL.Enums;
using System;
using System.Collections.Generic;

namespace SmartHome.BLL.Entities
{
    public class Sensor
    {
        public Guid Id { get; protected set; }
        public MeasurementType MeasurementType { get; protected set; }
        public MeasurementUnit MeasurementUnit { get; protected set; }
        public string CustomName { get; protected set; }
        public float LatestValue { get; protected set; }
        public IList<Measurement> Measurements { get; protected set; }
        //TODO change below to Value Objects
        public float? UpperTriggerLimit { get; protected set; }
        public float? LowerTriggerLimit { get; protected set; }

        public Sensor() 
        {
            Measurements = new List<Measurement>();
        }

        public Sensor(AddSensorDto addSensorDto) : this()
        {
            Id = addSensorDto.Id;
            MeasurementType = (MeasurementType)addSensorDto.MeasurementType;
            MeasurementUnit = (MeasurementUnit)addSensorDto.MeasurementUnit;
        }

        public Sensor(ReadMeasurementDto addMeasurementDto) : this()
        {
            Id = addMeasurementDto.SensorId;
            MeasurementType = (MeasurementType)addMeasurementDto.MeasurementType;
            MeasurementUnit = (MeasurementUnit)addMeasurementDto.MeasurementUnit;
            AddMeasurement(addMeasurementDto);
        }

        public void AddMeasurement(ReadMeasurementDto readMeasurementsDto)
        {
            var measurement = new Measurement(readMeasurementsDto);
            Measurements.Add(measurement);
        }

        public void ModifyTriggers(ChangeSensorTriggersDto changeSensorTriggersDto)
        {
            UpperTriggerLimit = changeSensorTriggersDto.UpperTriggerLimit;
            LowerTriggerLimit = changeSensorTriggersDto.LowerTriggerLimit;
        }

        public AlertCreateDto ConsumeMeasurement(float measurement)
        {
            LatestValue = measurement;
            var alertCreateDto = new AlertCreateDto() { SensorId = Id };

            if (!AreTriggersSet())
                return null;

            if (measurement >= UpperTriggerLimit && LatestValue <= UpperTriggerLimit)
                alertCreateDto.Trigger = Trigger.Upper;
            else if (measurement <= LowerTriggerLimit && LatestValue >= LowerTriggerLimit)
                alertCreateDto.Trigger = Trigger.Lower;
            else
                return null;

            var alertMessageTriggerText = alertCreateDto.Trigger == Trigger.Upper ? "above" : "below"; 

            alertCreateDto.AlertMessage = MeasurementType switch
            {
                MeasurementType.Temperature => $"Temperature {alertMessageTriggerText} limit.",
                MeasurementType.Humidity => $"Humidity {alertMessageTriggerText} limit.",
                _ => $"Unknown measurement type {alertMessageTriggerText} limit."
            };

            return alertCreateDto;
        }

        public void ChangeCustomName(string name)
        {
            CustomName = name;
        }

        private bool AreTriggersSet() =>
            UpperTriggerLimit is not null
            && LowerTriggerLimit is not null;
    }
}

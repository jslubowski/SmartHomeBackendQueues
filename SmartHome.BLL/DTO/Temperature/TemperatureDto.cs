using SmartHome.BLL.Enums;
using System;

namespace SmartHome.BLL.DTO.Temperature
{
    public class TemperatureDto
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public string SensorName { get; set; }
        public int Value { get; set; }
        public TemperatureUnit TemperatureUnit { get; set; }

        public TemperatureDto(Entities.Temperature temperature)
        {
            Id = temperature.Id;
            Date = temperature.Date;
            SensorName = temperature.SensorName;
            Value = temperature.Value;
            TemperatureUnit = temperature.TemperatureUnit;
        }
    }
}

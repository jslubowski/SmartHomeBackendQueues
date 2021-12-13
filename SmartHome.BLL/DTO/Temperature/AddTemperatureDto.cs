using System;

namespace SmartHome.BLL.DTO.Temperature
{
    public class AddTemperatureDto
    {
        public DateTime Date { get; set; }
        public string SensorName { get; set; }
        public int Value { get; set; }
        public int TemperatureUnit { get; set; }
    }
}

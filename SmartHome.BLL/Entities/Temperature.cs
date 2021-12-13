using SmartHome.BLL.DTO.Temperature;
using SmartHome.BLL.Enums;

namespace SmartHome.BLL.Entities
{
    public class Temperature : ReadingBase
    {
        public TemperatureUnit TemperatureUnit { get; protected set; }

        public Temperature() { }

        public Temperature(AddTemperatureDto addTemperatureDto)
        {
            Date = addTemperatureDto.Date;
            SensorName = addTemperatureDto.SensorName;
            Value = addTemperatureDto.Value;
            TemperatureUnit = (TemperatureUnit)addTemperatureDto.TemperatureUnit;
        }
    }
}

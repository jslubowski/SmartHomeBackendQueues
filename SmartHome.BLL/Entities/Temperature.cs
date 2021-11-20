using SmartHome.BLL.Enums;

namespace SmartHome.BLL.Entities
{
    public class Temperature : ReadingBase
    {
        public string SensorName { get; set; }
        public TemperatureUnit TemperatureUnit { get; set; }
    }
}

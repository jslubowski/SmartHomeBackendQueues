using SmartHome.BLL.DTO.Sensor;
using SmartHome.BLL.DTO.Temperature;
using System;
using System.Threading.Tasks;

namespace SmartHome.BLL.Services.Internal
{
    public interface ISensorService
    {
        Task<SensorDto> GetAsync(Guid sensorId);
        Task<SensorDto> AddAsync(AddSensorDto addSensorDto);
        Task<LatestValueSensorDto> GetLatestValue(Guid sensorId);
        Task SaveMeasurementAsync(AddMeasurementDto addMeasurementDto);
    }
}

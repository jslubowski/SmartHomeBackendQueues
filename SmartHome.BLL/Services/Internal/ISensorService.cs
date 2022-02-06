using SmartHome.BLL.DTO.Measurements;
using SmartHome.BLL.DTO.Sensor;
using SmartHome.BLL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHome.BLL.Services.Internal
{
    public interface ISensorService
    {
        Task<SensorDto> GetAsync(Guid sensorId);
        Task<SensorDto> AddAsync(AddSensorDto addSensorDto);
        Task<LatestValueSensorDto> GetLatestValue(Guid sensorId);
        Task<Sensor> SaveMeasurementAsync(Sensor sensor, ReadMeasurementDto addMeasurementDto);
        Task<SensorDto> ChangeSensorTriggersAsync(Guid sensorId, ChangeSensorTriggersDto changeSensorTriggersDto);
        Task<IEnumerable<SensorDto>> GetSensorsAsync();
        Task ReadMeasurementAsync(ReadMeasurementDto readMeasurementDto);
        Task<SensorDto> ChangeSensorNameAsync(Guid sensorId, ChangeSensorNameDto changeSensorNameDto);
    }
}

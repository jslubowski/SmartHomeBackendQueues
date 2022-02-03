using SmartHome.BLL.DTO.Sensor;
using SmartHome.BLL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHome.BLL.Repositories.Internal
{
    public interface ISensorRepository
    {
        Task SaveChangesAsync();
        Task<Sensor> GetAsync(Guid sensorId);
        Task AddAsync(Sensor sensor);
        Task<LatestValueSensorDto> GetLatestValue(Guid sensorId);
        Task<IEnumerable<SensorDto>> GetSensorsAsync();
    }
}

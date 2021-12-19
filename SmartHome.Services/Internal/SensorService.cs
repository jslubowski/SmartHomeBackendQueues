using SmartHome.BLL.DTO.Sensor;
using SmartHome.BLL.DTO.Temperature;
using SmartHome.BLL.Entities;
using SmartHome.BLL.Repositories.Internal;
using SmartHome.BLL.Services.Internal;
using System;
using System.Threading.Tasks;

namespace SmartHome.Services.Internal
{
    public class SensorService : ISensorService
    {
        private readonly ISensorRepository _sensorRepository;

        public SensorService(ISensorRepository sensorRepository)
        {
            _sensorRepository = sensorRepository;
        }

        public async Task<SensorDto> AddAsync(AddSensorDto addSensorDto)
        {
            var sensor = await _sensorRepository.GetAsync(addSensorDto.Id);

            if (sensor is not null)
                return new SensorDto(sensor);

            sensor = new Sensor(addSensorDto);
            await _sensorRepository.AddAsync(sensor);
            await _sensorRepository.SaveChangesAsync();

            return new SensorDto(sensor);
        }

        public async Task<SensorDto> GetAsync(Guid sensorId)
        {
            var sensor = await _sensorRepository.GetAsync(sensorId);

            if (sensor is null)
                return null;

            return new SensorDto(sensor);
        }

        public async Task<LatestValueSensorDto> GetLatestValue(Guid sensorId) => 
            await _sensorRepository.GetLatestValue(sensorId);

        public async Task SaveMeasurementAsync(AddMeasurementDto addMeasurementDto)
        {
            var sensor = await _sensorRepository.GetAsync(addMeasurementDto.SensorId);

            if (sensor is null)
            {
                sensor = new(addMeasurementDto);
                await _sensorRepository.AddAsync(sensor);
            }
            else
                sensor.AddMeasurement(addMeasurementDto);

            await _sensorRepository.SaveChangesAsync();
        }

    }
}

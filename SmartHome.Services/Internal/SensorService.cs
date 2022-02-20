using SmartHome.BLL.DTO.Measurements;
using SmartHome.BLL.DTO.Sensor;
using SmartHome.BLL.Entities;
using SmartHome.BLL.Repositories.Internal;
using SmartHome.BLL.Services.External;
using SmartHome.BLL.Services.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Services.Internal
{
    public class SensorService : ISensorService
    {
        private readonly ISensorRepository _sensorRepository;
        private readonly IMessageBusClient _messageBusClient;

        private const double saveMeasurementInterval = 5.0;

        public SensorService(ISensorRepository sensorRepository, IMessageBusClient messageBusClient)
        {
            _sensorRepository = sensorRepository;
            _messageBusClient = messageBusClient;
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

        public async Task<SensorDto> ChangeSensorTriggersAsync(Guid sensorId, ChangeSensorTriggersDto changeSensorTriggersDto)
        {
            var sensor = await _sensorRepository.GetAsync(sensorId);

            if (sensor is null)
                return null;

            sensor.ModifyTriggers(changeSensorTriggersDto);
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

        public async Task<IEnumerable<SensorDto>> GetSensorsAsync() =>
            await _sensorRepository.GetSensorsAsync();

        public async Task<LatestValueSensorDto> GetLatestValue(Guid sensorId) =>
            await _sensorRepository.GetLatestValue(sensorId);

        public async Task<Sensor> SaveMeasurementAsync(Sensor sensor, ReadMeasurementDto readMeasurementDto)
        {
            if (sensor is null)
            {
                sensor = new(readMeasurementDto);
                await _sensorRepository.AddAsync(sensor);
            }

            var latestMeasurement = await _sensorRepository.GetLatestMeasurementAsync(sensor.Id);
            var interval = DateTime.Now - latestMeasurement?.Date ?? TimeSpan.FromMinutes(2 * saveMeasurementInterval); // if null then exceed the save interval so the first measurement is saved

            if (interval > TimeSpan.FromMinutes(saveMeasurementInterval))
                sensor.AddMeasurement(readMeasurementDto);

            await _sensorRepository.SaveChangesAsync();
            return sensor;
        }

        public async Task ReadMeasurementAsync(ReadMeasurementDto readMeasurementDto)
        {
            var sensor = await _sensorRepository.GetAsync(readMeasurementDto.SensorId);

            var alertDto = sensor.ConsumeMeasurement(readMeasurementDto.Value);
            if (alertDto is not null)
                _messageBusClient.SendAlertAsync(alertDto);


            await SaveMeasurementAsync(sensor, readMeasurementDto);
        }

        public async Task<SensorDto> ChangeSensorNameAsync(Guid sensorId, ChangeSensorNameDto changeSensorNameDto)
        {
            var sensor = await _sensorRepository.GetAsync(sensorId);

            if (sensor is null)
                return null;

            sensor.ChangeCustomName(changeSensorNameDto.CustomName);

            await _sensorRepository.SaveChangesAsync();
            return new SensorDto(sensor);
        }
    }
}

using SmartHome.BLL.DTO.Measurements;
using SmartHome.BLL.DTO.Sensor;
using SmartHome.BLL.Entities;
using SmartHome.BLL.Repositories.Internal;
using SmartHome.BLL.Services.External;
using SmartHome.BLL.Services.Internal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHome.Services.Internal
{
    public class SensorService : ISensorService
    {
        private readonly ISensorRepository _sensorRepository;
        private readonly IMessageBusClient _messageBusClient;

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

        public async Task SaveMeasurementAsync(Sensor sensor, ReadMeasurementDto readMeasurementDto)
        {
            // TODO save only if older than 15 minutes
            if (sensor is null)
            {
                sensor = new(readMeasurementDto);
                await _sensorRepository.AddAsync(sensor);
            }
            else
                sensor.AddMeasurement(readMeasurementDto);

            await _sensorRepository.SaveChangesAsync();
        }

        public async Task ReadMeasurementAsync(ReadMeasurementDto readMeasurementDto)
        {
            var sensor = await _sensorRepository.GetAsync(readMeasurementDto.SensorId);
            _messageBusClient.SendAlertAsync(new BLL.DTO.AlertCreateDto { ActorId = Guid.NewGuid() });

            if (sensor is not null)
            {
                if (readMeasurementDto.Value > sensor.UpperTriggerLimit)
                    Console.WriteLine("---> Sending above threshold alert through Rabbit");
                else if (readMeasurementDto.Value < sensor.LowerTriggerLimit)
                    Console.WriteLine("---> Sending below threshold alert through Rabbit");
            }

            await SaveMeasurementAsync(sensor, readMeasurementDto);
        }
    }
}

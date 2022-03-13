using Microsoft.EntityFrameworkCore;
using SmartHome.BLL.DTO.Sensor;
using SmartHome.BLL.Entities;
using SmartHome.BLL.Repositories.Internal;
using SmartHome.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.DAL.Repositories
{
    public class SensorRepository : ISensorRepository
    {
        private readonly PostgresDbContext _postgresDbContext;

        public SensorRepository(PostgresDbContext postgresDbContext)
        {
            _postgresDbContext = postgresDbContext;
        }

        public async Task AddAsync(Sensor sensor) =>
            await _postgresDbContext.Sensors.AddAsync(sensor);

        public async Task<Sensor> GetAsync(Guid sensorId) =>
            await _postgresDbContext.Sensors
                .SingleOrDefaultAsync(sensor => sensor.Id == sensorId);

        public async Task<IEnumerable<SensorDto>> GetSensorsAsync() =>
            await _postgresDbContext.Sensors
                .AsNoTracking()
                .Select(sensor => new SensorDto(sensor))
                .ToListAsync();

        public async Task<LatestValueSensorDto> GetLatestValue(Guid sensorId) =>
            await _postgresDbContext.Sensors
                .Where(sensor => sensor.Id == sensorId)
                .Select(sensor => new LatestValueSensorDto(sensor))
                .SingleAsync();

        public async Task SaveChangesAsync() => await _postgresDbContext.SaveChangesAsync();

        public async Task<Measurement> GetLatestMeasurementAsync(Guid sensorId) =>
            await _postgresDbContext.Sensors
                .Include(sensor => sensor.Measurements)
                .Where(sensor => sensor.Id == sensorId)
                .SelectMany(sensor => sensor.Measurements)
                .OrderBy(measurement => measurement.Date)
                .LastOrDefaultAsync();
    }
}

﻿using Microsoft.EntityFrameworkCore;
using SmartHome.BLL.DTO.Sensor;
using SmartHome.BLL.Entities;
using SmartHome.BLL.Repositories.Internal;
using SmartHome.DAL.Data;
using System;
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


        public async Task<LatestValueSensorDto> GetLatestValue(Guid sensorId) =>
            await _postgresDbContext.Sensors
                .Select(sensor => new LatestValueSensorDto(sensor))
                .SingleOrDefaultAsync(sensor => sensor.Id == sensorId);

        public async Task SaveChangesAsync() => await _postgresDbContext.SaveChangesAsync();
    }
}
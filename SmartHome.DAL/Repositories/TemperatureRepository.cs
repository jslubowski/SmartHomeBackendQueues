using SmartHome.BLL.DTO.Temperature;
using SmartHome.BLL.Entities;
using SmartHome.BLL.Repositories;
using SmartHome.DAL.Data;
using System.Threading.Tasks;

namespace SmartHome.DAL.Repositories
{
    public class TemperatureRepository : ITemperatureRepository
    {
        private readonly PostgresDbContext _postgresDbContext;

        public TemperatureRepository(PostgresDbContext postgresDbContext)
        {
            _postgresDbContext = postgresDbContext;
        }

        public async Task AddTemperatureAsync(Temperature temperature)
        {
            await _postgresDbContext.Temperatures.AddAsync(temperature);
        }

        public async Task SaveChangesAsync()
        {
            await _postgresDbContext.SaveChangesAsync();
        }
    }
}

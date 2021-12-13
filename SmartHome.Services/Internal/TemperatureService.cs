using SmartHome.BLL.DTO.Temperature;
using SmartHome.BLL.Entities;
using SmartHome.BLL.Repositories;
using SmartHome.BLL.Services.Internal;
using System.Threading.Tasks;

namespace SmartHome.Services.Internal
{
    public class TemperatureService : ITemperatureService
    {
        private readonly ITemperatureRepository _temperatureRepository;

        public TemperatureService(ITemperatureRepository temperatureRepository)
        {
            _temperatureRepository = temperatureRepository;
        }

        public async Task<TemperatureDto> AddTemperatureAsync(AddTemperatureDto addTemperatureDto)
        {
            var temperature = new Temperature(addTemperatureDto);
            await _temperatureRepository.AddTemperatureAsync(temperature);
            await _temperatureRepository.SaveChangesAsync();
            return new TemperatureDto(temperature);
        }
    }
}

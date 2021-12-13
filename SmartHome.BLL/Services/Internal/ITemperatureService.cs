using SmartHome.BLL.DTO.Temperature;
using System.Threading.Tasks;

namespace SmartHome.BLL.Services.Internal
{
    public interface ITemperatureService
    {
        Task<TemperatureDto> AddTemperatureAsync(AddTemperatureDto addTemperatureDto);
    }
}

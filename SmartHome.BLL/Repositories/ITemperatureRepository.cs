using SmartHome.BLL.Entities;
using System.Threading.Tasks;

namespace SmartHome.BLL.Repositories
{
    public interface ITemperatureRepository
    {
        Task AddTemperatureAsync(Temperature temperature);
        Task SaveChangesAsync();
    }
}

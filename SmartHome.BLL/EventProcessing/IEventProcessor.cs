using System.Threading.Tasks;

namespace SmartHome.BLL.EventProcessing
{
    public interface IEventProcessor
    {
        Task ProcessEvent(string message);
    }
}

using SmartHome.BLL.DTO;

namespace SmartHome.BLL.Services.External
{
    public interface IMessageBusClient
    {
        public void SendAlertAsync(AlertCreateDto alertCreateDto);
    }
}

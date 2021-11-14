namespace SmartHome.BLL.EventProcessing
{
    public interface IEventProcessor
    {
        void ProcessMessage(string message);
    }
}

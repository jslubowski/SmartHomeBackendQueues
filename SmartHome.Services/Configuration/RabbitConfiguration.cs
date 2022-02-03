namespace SmartHome.Services.Configuration
{
    public class RabbitConfiguration
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string MeasurementsQueueName { get; set; }
        public string AlertsQueueName { get; set; }
    }
}

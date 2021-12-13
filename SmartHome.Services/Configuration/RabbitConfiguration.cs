namespace SmartHome.Services.Configuration
{
    public class RabbitConfiguration
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string TemperatureQueue { get; set; }
        public string HumidityQueue { get; set; }
    }
}

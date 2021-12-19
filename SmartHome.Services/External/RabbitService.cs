using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SmartHome.BLL.DTO.Temperature;
using SmartHome.BLL.Services.Internal;
using SmartHome.Services.Configuration;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartHome.Services.External
{
    public class RabbitService : BackgroundService
    {
        private readonly RabbitConfiguration _rabbitConfiguration;
        private readonly IServiceProvider _serviceProvider;
        private IConnection _connection;
        private IModel _channel;

        public RabbitService(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _rabbitConfiguration = configuration.GetSection("RabbitMQ").Get<RabbitConfiguration>();
            _serviceProvider = serviceProvider;

            Initialize();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var temperatureConsumer = new EventingBasicConsumer(_channel);
            temperatureConsumer.Received += async (_, ea) =>
            {
                Console.WriteLine("--> Temperature Event Received!");

                var body = ea.Body;
                var temperatureMessage = Encoding.UTF8.GetString(body.ToArray());
                var addMeasurementDto = JsonConvert.DeserializeObject<AddMeasurementDto>(temperatureMessage);

                using (var scope = _serviceProvider.CreateScope())
                {
                    var temperatureService = scope.ServiceProvider.GetRequiredService<ISensorService>();
                    await temperatureService.SaveMeasurementAsync(addMeasurementDto);
                }
            };

            var humidityConsumer = new EventingBasicConsumer(_channel);
            humidityConsumer.Received += (_, ea) =>
            {
                Console.WriteLine("--> Humidity Event Received!");

                var body = ea.Body;
                var humidityMessage = Encoding.UTF8.GetString(body.ToArray());
            };

            _channel.BasicConsume(
                    queue: _rabbitConfiguration.TemperatureQueue,
                    autoAck: true,
                    consumer: temperatureConsumer
                );

            _channel.BasicConsume(
                    queue: _rabbitConfiguration.HumidityQueue,
                    autoAck: true,
                    consumer: humidityConsumer
                );

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }

            base.Dispose();
        }

        private void Initialize()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _rabbitConfiguration.Host,
                Port = int.Parse(_rabbitConfiguration.Port),
                UserName = _rabbitConfiguration.Username,
                Password = _rabbitConfiguration.Password,
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueBind(
                    queue: _rabbitConfiguration.TemperatureQueue,
                    exchange: "amq.topic",
                    routingKey: $"/{_rabbitConfiguration.TemperatureQueue}"
                );
            _channel.QueueBind(
                    queue: "backend",
                    exchange: "amq.topic",
                    routingKey: $"/{_rabbitConfiguration.HumidityQueue}"
                );
            Console.WriteLine("--> Listening on the Message Bus");
            _connection.ConnectionShutdown += ConnectionShutdown;
        }

        private void ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("--> Connection Shutdown");
        }
    }
}

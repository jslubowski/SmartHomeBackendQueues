using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
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
        private IConnection _connection;
        private IModel _channel;

        public RabbitService(IConfiguration configuration)
        {
            _rabbitConfiguration = configuration.GetSection("RabbitMQ").Get<RabbitConfiguration>();

            Initialize();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ModuleHandle, ea) =>
            {
                Console.WriteLine("--> Event Received!");
                var body = ea.Body;
                var notificationMessage = Encoding.UTF8.GetString(body.ToArray());

                Console.WriteLine(notificationMessage);
            };

            _channel.BasicConsume(
                    queue: _rabbitConfiguration.Queue,
                    autoAck: true,
                    consumer: consumer
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
                    queue: _rabbitConfiguration.Queue,
                    exchange: "amq.topic",
                    routingKey: $"/{_rabbitConfiguration.Queue}"
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

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SmartHome.BLL.DTO;
using SmartHome.BLL.EventProcessing;
using SmartHome.Services.Configuration;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartHome.Services.External
{
    public class MessageBusListener : BackgroundService
    {
        private readonly RabbitConfiguration _rabbitConfiguration;
        private readonly IEventProcessor _eventProcessor;
        private IConnection _connection;
        private IModel _measurementChannel;

        public MessageBusListener(IConfiguration configuration, IEventProcessor eventProcessor)
        {
            _rabbitConfiguration = configuration.GetSection("RabbitMQ").Get<RabbitConfiguration>();
            _eventProcessor = eventProcessor;

            Initialize();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var measurementConsumer = new EventingBasicConsumer(_measurementChannel);
            measurementConsumer.Received += async (_, ea) => await MeasurementConsumer(ea);
            _measurementChannel.BasicConsume(
                    queue: _rabbitConfiguration.MeasurementsQueueName,
                    autoAck: true,
                    consumer: measurementConsumer
                );

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            if (_measurementChannel.IsOpen)
            {
                _measurementChannel.Close();
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

            _measurementChannel = _connection.CreateModel();
            _measurementChannel.QueueBind(
                    queue: _rabbitConfiguration.MeasurementsQueueName,
                    exchange: "amq.topic",
                    routingKey: $"backend/{_rabbitConfiguration.MeasurementsQueueName}"
                );

            Console.WriteLine("--> Listening on the Message Bus");
            _connection.ConnectionShutdown += ConnectionShutdown;
        }

        private void ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("--> Connection Shutdown");
        }

        private async Task MeasurementConsumer(BasicDeliverEventArgs ea)
        {
            Console.WriteLine("--> Measurement Event Received!");

            var body = ea.Body;
            var measurementMessage = Encoding.UTF8.GetString(body.ToArray());
            await _eventProcessor.ProcessEvent(measurementMessage);
        }
    }
}

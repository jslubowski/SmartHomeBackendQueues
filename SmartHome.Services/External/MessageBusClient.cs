using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using SmartHome.BLL.DTO;
using SmartHome.BLL.Services.External;
using SmartHome.Services.Configuration;
using System;
using System.Text;

namespace SmartHome.Services.External
{
    public class MessageBusClient : IMessageBusClient, IDisposable
    {
        private readonly RabbitConfiguration _rabbitConfiguration;
        private IConnection _connection;
        private IModel _alertsChannel;

        public MessageBusClient(IConfiguration configuration)
        {
            _rabbitConfiguration = configuration.GetSection("RabbitMQ").Get<RabbitConfiguration>();

            Initialize();
        }

        public void Dispose()
        {
            if (_alertsChannel.IsOpen)
            {
                _alertsChannel.Close();
                _connection.Close();
            }

            GC.SuppressFinalize(this);
        }

        public void SendAlertAsync(AlertCreateDto alertCreateDto)
        {
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(alertCreateDto));
            _alertsChannel.BasicPublish(
                exchange: "amq.topic",
                routingKey: $"/{_rabbitConfiguration.AlertsQueueName}",
                basicProperties: null,
                body: body
                );
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

            _alertsChannel = _connection.CreateModel();
            _alertsChannel.QueueBind(
                    queue: _rabbitConfiguration.MeasurementsQueueName,
                    exchange: "amq.topic",
                    routingKey: $"/{_rabbitConfiguration.AlertsQueueName}"
                );
        }
    }
}

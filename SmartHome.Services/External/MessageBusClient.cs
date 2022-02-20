using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SmartHome.BLL.DTO;
using SmartHome.BLL.Services.External;
using SmartHome.Services.Configuration;
using System;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace SmartHome.Services.External
{
    public class MessageBusClient : IMessageBusClient, IDisposable
    {
        private readonly RabbitConfiguration _rabbitConfiguration;
        private MqttClient _mqttClient;

        private const string _backendId = "BACKEND_ID_fbc1d301-7dd1-4d28-a5d0-fd13410cbf0a";

        public MessageBusClient(IConfiguration configuration)
        {
            _rabbitConfiguration = configuration.GetSection("RabbitMQ").Get<RabbitConfiguration>();

            Initialize();
        }

        public void Dispose()
        {
            _mqttClient.Disconnect();

            GC.SuppressFinalize(this);
        }

        public void SendAlertAsync(AlertCreateDto alertCreateDto)
        {
            var contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(alertCreateDto, new JsonSerializerSettings()
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            }));

            _mqttClient.Publish(
                 _rabbitConfiguration.AlertsQueueName,
                 body,
                 MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE,
                 false
                );
        }

        private void Initialize()
        { 
            _mqttClient = new MqttClient(_rabbitConfiguration.Host);
            _mqttClient.Connect(_backendId, _rabbitConfiguration.Username, _rabbitConfiguration.Password);
        }
    }
}

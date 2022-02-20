using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SmartHome.BLL.DTO.Measurements;
using SmartHome.BLL.EventProcessing;
using SmartHome.BLL.Services.Internal;
using SmartHome.Services.Enums;
using System;
using System.Threading.Tasks;

namespace SmartHome.Services.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private IServiceScopeFactory _serviceScopeFactory;

        public EventProcessor(IServiceScopeFactory scopeFactory)
        {
            _serviceScopeFactory = scopeFactory;
        }

        public async Task ProcessEvent(string message)
        {
            try
            {
                var eventType = DetermineEvent(message);

                switch (eventType)
                {
                    case EventType.MeasurementPublished:
                        await ReadMeasurement(message);
                        break;
                    default:
                        Console.WriteLine("Unknown event");
                        break;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error while reading body of the message: {message}");
                Console.WriteLine(ex.Message);
            }
        }

        private async Task ReadMeasurement(string message)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var sensorService = scope.ServiceProvider.GetRequiredService<ISensorService>();
                var addMeasurementDto = JsonConvert.DeserializeObject<ReadMeasurementDto>(message);
                await sensorService.ReadMeasurementAsync(addMeasurementDto);
            }
        }

        private EventType DetermineEvent(string message) =>
            JsonConvert.DeserializeObject<GenericEventDto>(message).EventType
            switch
            {
                "measurement_published" => EventType.MeasurementPublished,
                _ => EventType.Undetermined
            };
    }
}

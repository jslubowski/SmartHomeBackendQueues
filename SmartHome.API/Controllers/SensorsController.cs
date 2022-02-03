using Microsoft.AspNetCore.Mvc;
using SmartHome.BLL.DTO.Sensor;
using SmartHome.BLL.Services.Internal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHome.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SensorsController : ControllerBase
    {
        private readonly ISensorService _sensorService;

        public SensorsController(ISensorService sensorService)
        {
            _sensorService = sensorService;
        }

        [HttpGet]
        public async Task<IEnumerable<SensorDto>> GetSensorsAsync() => 
            await _sensorService.GetSensorsAsync();

        [HttpGet("{sensorId}")]
        public async Task<ActionResult<SensorDto>> GetAsync(Guid sensorId)
        {
            var sensor = await _sensorService.GetAsync(sensorId);
            return OkOrNoContent(sensor);
        }

        // TODO : endpoint to change custom name

        [HttpPatch("{sensorId}/triggers")]
        public async Task<ActionResult<SensorDto>> ChangeTriggersAsync([FromRoute] Guid sensorId, [FromBody] ChangeSensorTriggersDto changeSensorTriggersDto)
        {
            var sensor = await _sensorService.ChangeSensorTriggersAsync(sensorId, changeSensorTriggersDto);
            return OkOrNoContent(sensor);
        }

        private ActionResult<T> OkOrNoContent<T>(T obj) => 
            obj switch
            {
                null => NoContent(),
                _ => Ok(obj)
            };
    }
}

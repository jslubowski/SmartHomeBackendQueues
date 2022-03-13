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
        public async Task<ActionResult<IEnumerable<SensorDto>>> GetSensorsAsync() => 
            Ok(await _sensorService.GetSensorsAsync());

        [HttpGet("{sensorId}")]
        public async Task<ActionResult<SensorDto>> GetAsync(Guid sensorId)
        {
            var sensor = await _sensorService.GetAsync(sensorId);
            return OkOrNoContent(sensor);
        }

        [HttpGet("{sensorId}/current-value")]
        public async Task<ActionResult<LatestValueSensorDto>> GetCurrentValue(Guid sensorId) => 
            OkOrNoContent(await _sensorService.GetLatestValue(sensorId));

        [HttpPatch("{sensorId}/triggers")]
        public async Task<ActionResult<SensorDto>> ChangeTriggersAsync([FromRoute] Guid sensorId, [FromBody] ChangeSensorTriggersDto changeSensorTriggersDto)
        {
            var sensor = await _sensorService.ChangeSensorTriggersAsync(sensorId, changeSensorTriggersDto);
            return OkOrNoContent(sensor);
        }

        [HttpPatch("{sensorId}/custom-name")]
        public async Task<ActionResult<SensorDto>> ChangeCustomNameAsync([FromRoute] Guid sensorId, [FromBody] ChangeSensorNameDto changeSensorNameDto)
        {
            var sensor = await _sensorService.ChangeSensorNameAsync(sensorId, changeSensorNameDto);
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

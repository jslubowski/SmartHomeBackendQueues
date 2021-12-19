using Microsoft.AspNetCore.Mvc;
using SmartHome.BLL.DTO.Sensor;
using SmartHome.BLL.Services.Internal;
using System;
using System.Threading.Tasks;

namespace SmartHome.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SensorContoller : ControllerBase
    {
        private readonly ISensorService _sensorService;

        public SensorContoller(ISensorService sensorService)
        {
            _sensorService = sensorService;
        }

        [HttpGet("{sensorId}")]
        public async Task<ActionResult<SensorDto>> GetAsync(Guid sensorId)
        {
            var sensor = await _sensorService.GetAsync(sensorId);
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

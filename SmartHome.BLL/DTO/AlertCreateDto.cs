using SmartHome.BLL.Enums;
using System;

namespace SmartHome.BLL.DTO
{
    public class AlertCreateDto
    {
        public Guid SensorId { get; set; }
        public string AlertMessage { get; set; }
        public Trigger Trigger { get; set; }
    }
}

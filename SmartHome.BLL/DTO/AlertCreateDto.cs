using System;

namespace SmartHome.BLL.DTO
{
    public class AlertCreateDto
    {
        public Guid ActorId { get; set; }
        public bool ActorState { get; set; }
        public string Event { get; set; }
    }
}

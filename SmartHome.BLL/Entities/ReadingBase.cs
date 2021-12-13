using System;
using System.ComponentModel.DataAnnotations;

namespace SmartHome.BLL.Entities
{
    public abstract class ReadingBase
    {
        public long Id { get; protected set; }
        public DateTime Date { get; protected set; }
        public string SensorName { get; protected set; }
        public int Value { get; protected set; }
    }
}

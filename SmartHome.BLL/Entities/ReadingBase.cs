using System;
using System.ComponentModel.DataAnnotations;

namespace SmartHome.BLL.Entities
{
    public abstract class ReadingBase
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public int Value { get; set; }
    }
}

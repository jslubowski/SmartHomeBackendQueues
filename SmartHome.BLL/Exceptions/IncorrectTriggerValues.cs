using System;

namespace SmartHome.BLL.Exceptions
{
    public class IncorrectTriggerValues : Exception
    {
        public IncorrectTriggerValues(string message) : base(message) { }
    }
}

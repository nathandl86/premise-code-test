using System;

namespace DutyHours.Models.Interfaces
{
    public interface ILogger
    {
        void Write(Exception ex);
    }
}
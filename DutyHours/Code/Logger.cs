using DutyHours.Models.Interfaces;
using System;
using System.Diagnostics;

namespace DutyHours.Code
{
    /// <summary>
    /// Class to wrap interactions with the EventLog class in 
    /// System.Diagnostics
    /// </summary>
    public class Logger : ILogger
    {
        /// <summary>
        /// Method to write application exceptions to the server 
        /// event log.
        /// </summary>
        /// <param name="ex"></param>
        public void Write(Exception ex)
        {
            var log = new EventLog();
            log.Source = "Application";
            log.WriteEntry(ex.Message, EventLogEntryType.Error);
        }
    }
}
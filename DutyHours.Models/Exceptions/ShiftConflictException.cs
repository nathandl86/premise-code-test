
using System;
using System.Collections.Generic;

namespace DutyHours.Models.Exceptions
{
    /// <summary>
    /// Exception wrapper for shift conflicts during save
    /// </summary>
    public class ShiftConflictException : Exception
    {
        public IList<ResidentShiftModel> Conflicts { get; set; }
    }
}

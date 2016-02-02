using System.Collections.Generic;
using DutyHours.Models;
using System;

namespace DutyHours.Services
{
    public interface IResidentService
    {
        // NOTE: THIS SECTION CONTAINS CHANGES ADDED AFTER THE 
        //    DEADLINE, TO RESOLVE BUG PERSISTING TIME ENTRIES
        ResponseModel<Tuple<ResidentShiftModel, IEnumerable<ResidentShiftModel>>> SaveShift(ResidentShiftModel shift);
    }
}
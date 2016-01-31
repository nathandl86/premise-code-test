using System.Collections.Generic;
using DutyHours.Models;
using DutyHours.Models.Data;

namespace DutyHours.Services
{
    public interface IResidentService
    {
        ResponseModel<IEnumerable<ResidentShift>> SaveShift(ResidentShift shift, bool overrideAcknowledged = false);
    }
}
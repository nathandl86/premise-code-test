using System.Collections.Generic;
using DutyHours.Models;

namespace DutyHours.Services
{
    public interface IResidentService
    {
        ResponseModel<IEnumerable<ResidentShiftModel>> SaveShift(ResidentShiftModel shift);
    }
}
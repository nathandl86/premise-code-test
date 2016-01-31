using System.Collections.Generic;
using DutyHours.Models;
using DutyHours.Models.Data;

namespace DutyHours.Data.Repsitories
{
    public interface IResidentRepository
    {
        ResponseModel<ResidentShift> Save(ResidentShift model);
        ResponseModel Delete(ResidentShift model);
        ResponseModel<InstitutionResident> FindById(int userId);
        ResponseModel<IEnumerable<ResidentShift>> FindShiftsByResidentId(int userId, int numberOfShifts = 90);
    }
}
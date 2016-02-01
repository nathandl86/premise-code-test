using System.Collections.Generic;
using DutyHours.Models;

namespace DutyHours.EntityData.Repsitories
{
    public interface IResidentRepository
    {
        ResponseModel<ResidentShiftModel> Save(ResidentShiftModel model);
        ResponseModel Delete(ResidentShiftModel model);
        ResponseModel<InstitutionResidentModel> FindById(int userId);
        ResponseModel<IEnumerable<ResidentShiftModel>> FindShiftsByResidentId(int userId, int numberOfShifts = 90);
    }
}
using System.Collections.Generic;
using DutyHours.Models;

namespace DutyHours.Data.Repsitories
{
    public interface IInstitutionRepository
    {
        ResponseModel<InstitutionModel> Find(int id);
        ResponseModel<IEnumerable<InstitutionModel>> FindAll();
        ResponseModel<IEnumerable<InstitutionResidentModel>> FindResidentsByInstitutionId(int institutionId);
    }
}
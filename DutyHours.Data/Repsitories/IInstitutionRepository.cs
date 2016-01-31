using System.Collections.Generic;
using DutyHours.Models;
using DutyHours.Models.Data;

namespace DutyHours.Data.Repsitories
{
    public interface IInstitutionRepository
    {
        ResponseModel<Institution> Find(int id);
        ResponseModel<IEnumerable<Institution>> FindAll();
        ResponseModel<IEnumerable<InstitutionResident>> FindResidentsByInstitutionId(int institutionId);
    }
}
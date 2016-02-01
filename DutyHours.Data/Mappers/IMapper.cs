using System.Collections.Generic;
using DutyHours.Data.Models;
using DutyHours.Models;

namespace DutyHours.Data.Mappers
{
    public interface IMapper
    {
        IEnumerable<ResidentShiftModel> Map(IEnumerable<ResidentShift> data);
        InstitutionModel Map(Institution data);
        InstitutionResidentModel Map(InstitutionResident data);
        ResidentShiftModel Map(ResidentShift data);
        UserModel Map(User data);
        User Map(UserModel model);
        ResidentShift Map(ResidentShiftModel model);
        InstitutionResident Map(InstitutionResidentModel model);
        Institution Map(InstitutionModel model);
        IEnumerable<UserModel> Map(IEnumerable<User> data);
        IEnumerable<InstitutionResidentModel> Map(IEnumerable<InstitutionResident> data);
        IEnumerable<InstitutionModel> Map(IEnumerable<Institution> data);
    }
}
using System.Data.Entity;
using DutyHours.Data.Models;

namespace DutyHours.Data
{
    public interface IDutyHoursDbContext
    {
        IDbSet<InstitutionAdmin> InstitutionAdmins { get; set; }
        IDbSet<InstitutionResident> InstitutionResidents { get; set; }
        IDbSet<Institution> Institutions { get; set; }
        IDbSet<ResidentShift> ResidentShifts { get; set; }
        IDbSet<User> Users { get; set; }

        int SaveChanges();
    }
}
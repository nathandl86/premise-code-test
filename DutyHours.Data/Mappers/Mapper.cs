using DutyHours.Data.Models;
using DutyHours.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DutyHours.Data.Mappers
{
    /// <summary>
    /// Mapper class to get plain models from entity models and vice-versa
    /// </summary>
    public class Mapper : IMapper
    {
        #region Insititution Resident

        public InstitutionResident Map(InstitutionResidentModel model)
        {
            var data = new InstitutionResident
            {
                Id = model.Id,
                InstitutionId = model.InstitutionId,
                UserId = model.UserId,

            };

            if (model.User != null)
            {
                data.User = Map(model.User);
            }

            return data;
        }

        public InstitutionResidentModel Map(InstitutionResident data)
        {
            var model = new InstitutionResidentModel
            {
                Id = data.Id,
                InstitutionId = data.InstitutionId,
                UserId = data.UserId
            };

            if (data.User != null)
            {
                model.User = Map(data.User);
            }

            return model;
        }

        public IEnumerable<InstitutionResidentModel> Map(IEnumerable<InstitutionResident> data)
        {
            return data.Select(Map);
        }

        #endregion

        #region User

        public User Map(UserModel model)
        {
            return new User
            {
                Id = model.Id,
                Active = model.IsActive,
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName
            };
        }

        public UserModel Map(User data)
        {
            return new UserModel
            {
                Id = data.Id,
                FirstName = data.FirstName,
                LastName = data.LastName,
                MiddleName = data.MiddleName,
                IsActive = data.Active,
                InstitutionsWhereAdmin = data.InstitutionAdmins.Where(ia => ia.UserId == data.Id).Select(ia => ia.InstitutionId).ToList()
            };
        }

        public IEnumerable<UserModel> Map(IEnumerable<User> data)
        {
            return data.Select(Map);
        }

        #endregion

        #region Institution

        public Institution Map(InstitutionModel model)
        {
            return new Institution
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        public InstitutionModel Map(Institution data)
        {
            return new InstitutionModel
            {
                Id = data.Id,
                Name = data.Name
            };
        }

        public IEnumerable<InstitutionModel> Map(IEnumerable<Institution> data)
        {
            return data.Select(Map);
        }

        #endregion

        #region Resident Shifts

        public ResidentShift Map(ResidentShiftModel model)
        {
            return new ResidentShift
            {
                Id = model.Id,
                InstitutionResidentId = model.InstitutionResidentId,

                //Ensure that the client times are mapped to UTC before persistence
                EndDateTimeUtc = model.EndDateTime.HasValue ? model.EndDateTime.Value.ToUniversalTime() : (DateTime?)null,
                StartDateTimeUtc = model.StartDateTime.ToUniversalTime(),
                EntryDateTimeUtc = model.EntryDateTime.ToUniversalTime()
            };
        }

        public ResidentShiftModel Map(ResidentShift data)
        {
            return new ResidentShiftModel
            {
                Id = data.Id,
                EndDateTime = data.EntryDateTimeUtc,
                StartDateTime = data.StartDateTimeUtc,
                EntryDateTime = data.EntryDateTimeUtc,
                InstitutionResidentId = data.InstitutionResidentId
            };
        }

        public IEnumerable<ResidentShiftModel> Map(IEnumerable<ResidentShift> data)
        {
            return data.Select(Map);
        }

        #endregion
    }
}

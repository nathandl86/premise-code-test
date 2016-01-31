
using DutyHours.Models;
using DutyHours.Models.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DutyHours.Data.Repsitories
{
    /// <summary>
    /// Entity Framework repository implementation of IResidentRepository.
    /// </summary>
    public class ResidentEntityRepository : EntityRepositoryBase, IResidentRepository
    {
        /// <summary>
        /// Constructor allowing data context to be injected
        /// </summary>
        /// <param name="dhDbContext"></param>
        public ResidentEntityRepository(IDutyHoursDbContext dhDbContext)
            : base(dhDbContext)
        { }

        #region Methods

        /// <summary>
        /// Method to get the resident by user id
        /// </summary>
        /// <param name="institutionResidentId">Institution Resident identifier</param>
        /// <returns></returns>
        public ResponseModel<InstitutionResident> FindById(int institutionResidentId)
        {
            Func<InstitutionResident> resolver = () =>
            {
                return DhDataContext
                    .InstitutionResidents
                    .Include(e => e.User)
                    .AsNoTracking()
                    .AsQueryable()
                    .Where(ir => ir.Id == institutionResidentId)
                    .FirstOrDefault();
            };

            return Retrieve(resolver);
        }

        /// <summary>
        /// Method to get resident shift history. 
        /// </summary>
        /// <param name="institutionResidentId">Institution Resident identifier</param>
        /// <param name="numberOfShifts">Limits the number of shifts retrieved. Defaults to 90</param>
        /// <returns></returns>
        public ResponseModel<IEnumerable<ResidentShift>> FindShiftsByResidentId(int institutionResidentId, int numberOfShifts = 90)
        {
            Func<IEnumerable<ResidentShift>> resolver = () =>
            {
                return DhDataContext
                    .ResidentShifts
                    .Include(e => e.InstitutionResident)
                    .AsNoTracking()
                    .AsQueryable()
                    .Where(rs => rs.InstitutionResidentId == institutionResidentId)
                    .OrderByDescending(rs => rs.StartDateTimeUtc)
                    .Take(numberOfShifts);
            };
            return RetrieveMany(resolver);
        }        

        /// <summary>
        /// Method to save a resident's shift details.
        /// Will perform intsert for new records and update existing.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResponseModel<ResidentShift> Save(ResidentShift model)
        {
            Func<ResidentShift> resolver = () =>
            {
                if (model.Id > 0)
                {
                    DhDataContext.ResidentShifts.Attach(model);
                }
                else
                {
                    DhDataContext.ResidentShifts.Add(model);
                }

                DhDataContext.SaveChanges();
                return model;
            };

            return Persist(resolver);
        }

        /// <summary>
        /// Method to remove a resident shift.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResponseModel Delete(ResidentShift model)
        {
            Action resolver = () =>
            {
                DhDataContext.ResidentShifts.Remove(model);
                DhDataContext.SaveChanges();
            };
            return Persist(resolver);
        }

        #endregion
    }
}

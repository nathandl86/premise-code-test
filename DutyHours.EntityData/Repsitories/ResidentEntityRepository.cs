
using DutyHours.EntityData;
using DutyHours.EntityData.Mappers;
using DutyHours.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DutyHours.EntityData.Repsitories
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
        public ResidentEntityRepository(DutyHoursModel dhDbContext, IMapper mapper)
            : base(dhDbContext, mapper)
        { }

        #region Methods

        /// <summary>
        /// Method to get the resident by user id
        /// </summary>
        /// <param name="institutionResidentId">Institution Resident identifier</param>
        /// <returns></returns>
        public ResponseModel<InstitutionResidentModel> FindById(int institutionResidentId)
        {
            Func<InstitutionResidentModel> resolver = () =>
            {
                var data = DhDataContext
                    .InstitutionResidents
                    .Include(e => e.User)
                    .AsQueryable()
                    .Where(ir => ir.Id == institutionResidentId)
                    .FirstOrDefault();
                return Mapper.Map(data);
            };

            return Retrieve(resolver);
        }

        /// <summary>
        /// Method to get resident shift history. 
        /// </summary>
        /// <param name="institutionResidentId">Institution Resident identifier</param>
        /// <param name="numberOfShifts">Limits the number of shifts retrieved. Defaults to 90</param>
        /// <returns></returns>
        public ResponseModel<IEnumerable<ResidentShiftModel>> FindShiftsByResidentId(int institutionResidentId, int numberOfShifts = 90)
        {
            Func<IEnumerable<ResidentShiftModel>> resolver = () =>
            {
                var data = DhDataContext
                    .ResidentShifts
                    .Include(e => e.InstitutionResident)
                    .AsQueryable()
                    .Where(rs => rs.InstitutionResidentId == institutionResidentId)
                    .OrderByDescending(rs => rs.StartDateTimeUtc)
                    .Take(numberOfShifts);
                return Mapper.Map(data);
            };
            return RetrieveMany(resolver);
        }

        /// <summary>
        /// Method to save a resident's shift details.
        /// Will perform intsert for new records and update existing.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResponseModel<ResidentShiftModel> Save(ResidentShiftModel model)
        {

            Func<ResidentShiftModel> resolver = () =>
            {
                var data = Mapper.Map(model);

                // NOTE: THIS SECTION CONTAINS CHANGES ADDED AFTER THE 
                //    DEADLINE, TO RESOLVE BUG PERSISTING TIME ENTRIES
                if (data.Id > 0)
                {
                    var dataEntity = DhDataContext.ResidentShifts.FirstOrDefault(e => e.Id == data.Id);
                    dataEntity.StartDateTimeUtc = data.StartDateTimeUtc;
                    dataEntity.EndDateTimeUtc = data.EndDateTimeUtc;
                    DhDataContext.Entry(dataEntity).State = EntityState.Modified;    
                }
                else
                {
                    DhDataContext.ResidentShifts.Add(data);
                }
                
                DhDataContext.SaveChanges();

                var affectedRecords = DhDataContext.SaveChanges();
                return Mapper.Map(data);
            };

            return Persist(resolver);
        }

        /// <summary>
        /// Method to remove a resident shift.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResponseModel Delete(ResidentShiftModel model)
        {
            Action resolver = () =>
            {
                var data = Mapper.Map(model);
                DhDataContext.ResidentShifts.Remove(data);
                DhDataContext.SaveChanges();
            };
            return Persist(resolver);
        }

        #endregion
    }
}

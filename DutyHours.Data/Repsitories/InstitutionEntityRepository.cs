using DutyHours.Models;
using DutyHours.Models.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DutyHours.Data.Repsitories
{
    /// <summary>
    /// Entity Framework repository implementing IInstitutionRepository
    /// </summary>
    public class InstitutionEntityRepository : EntityRepositoryBase, IInstitutionRepository
    {
        /// <summary>
        /// Constructor for DbContext to be injected
        /// </summary>
        /// <param name="dhDbContext"></param>
        public InstitutionEntityRepository(IDutyHoursDbContext dhDbContext)
            : base(dhDbContext) { } 

        /// <summary>
        /// Method to get the institutions from the database
        /// </summary>
        /// <returns></returns>
        public ResponseModel<IEnumerable<Institution>> FindAll()
        {
            Func<IEnumerable<Institution>> resolver = () =>
            {
                return DhDataContext
                    .Institutions
                    .AsNoTracking();
            };
            return RetrieveMany(resolver);
        }

        /// <summary>
        /// Method to get a specific institution from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseModel<Institution> Find(int id)
        {
            Func<Institution> resolver = () =>
            {
                return DhDataContext
                    .Institutions
                    .AsNoTracking()
                    .Where(i => i.Id == id)
                    .FirstOrDefault();
            };
            return Retrieve(resolver);
        }

        /// <summary>
        /// Method to get the residents within an institution
        /// </summary>
        /// <param name="institutionId"></param>
        /// <returns></returns>
        public ResponseModel<IEnumerable<InstitutionResident>> FindResidentsByInstitutionId(int institutionId)
        {
            Func<IEnumerable<InstitutionResident>> resolver = () =>
            {
                return DhDataContext
                    .InstitutionResidents
                    .Include(e => e.User)
                    .AsNoTracking()
                    .AsQueryable()
                    .Where(e => e.InstitutionId == institutionId)
                    .ToList();
            };
            return RetrieveMany(resolver);
        }
    }
}

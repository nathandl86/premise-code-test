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
    /// Entity Framework repository implementing IInstitutionRepository
    /// </summary>
    public class InstitutionEntityRepository : EntityRepositoryBase, IInstitutionRepository
    {
        /// <summary>
        /// Constructor for DbContext to be injected
        /// </summary>
        /// <param name="dhDbContext"></param>
        public InstitutionEntityRepository(DutyHoursModel dhDbContext, IMapper mapper)
            : base(dhDbContext, mapper) { } 

        /// <summary>
        /// Method to get the institutions from the database
        /// </summary>
        /// <returns></returns>
        public ResponseModel<IEnumerable<InstitutionModel>> FindAll()
        {
            Func<IEnumerable<InstitutionModel>> resolver = () =>
            {

                var data = DhDataContext
                    .Institutions
                    .Select(i=> i);

                return Mapper.Map(data);
            };
            return RetrieveMany(resolver);
        }

        /// <summary>
        /// Method to get a specific institution from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseModel<InstitutionModel> Find(int id)
        {
            Func<InstitutionModel> resolver = () =>
            {
                var data = DhDataContext
                    .Institutions
                    .Where(i => i.Id == id)
                    .FirstOrDefault();

                return Mapper.Map(data);
            };
            return Retrieve(resolver);
        }

        /// <summary>
        /// Method to get the residents within an institution
        /// </summary>
        /// <param name="institutionId"></param>
        /// <returns></returns>
        public ResponseModel<IEnumerable<InstitutionResidentModel>> FindResidentsByInstitutionId(int institutionId)
        {
            Func<IEnumerable<InstitutionResidentModel>> resolver = () =>
            {
                var data = DhDataContext
                    .InstitutionResidents
                    .Include(e => e.User)
                    .AsQueryable()
                    .Where(e => e.InstitutionId == institutionId);
                return Mapper.Map(data);
            };
            return RetrieveMany(resolver);
        }
    }
}

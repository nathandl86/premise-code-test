using DutyHours.EntityData.Mappers;
using DutyHours.EntityData.Repsitories;
using DutyHours.Models;
using DutyHours.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace DutyHours.Services
{
    /// <summary>
    /// Service implementation for resident logic processing
    /// </summary>
    public class ResidentService : ServiceBase, IResidentService
    {
        private readonly IResidentRepository _residentRepo;

        /// <summary>
        /// Constructor with injectibles 
        /// </summary>
        /// <param name="residentRepo"></param>
        public ResidentService(IResidentRepository residentRepo)
        {
            _residentRepo = residentRepo;
        }

        /// <summary>
        /// Method to save a resident's shift
        /// </summary>
        /// <param name="shift"></param>
        /// <param name="overrideAcknowledged">Indication the user has chosen to overwrite existing shifts</param>
        /// <returns></returns>
        public ResponseModel<Tuple<ResidentShiftModel, IEnumerable<ResidentShiftModel>>> SaveShift(ResidentShiftModel shift)
        {
            // NOTE: THIS SECTION CONTAINS CHANGES ADDED AFTER THE 
            //    DEADLINE, TO RESOLVE BUG PERSISTING TIME ENTRIES
            Func<Tuple<ResidentShiftModel, IEnumerable<ResidentShiftModel>>> func = () =>
            {
                var shifts = _residentRepo.FindShiftsByResidentId(shift.InstitutionResidentId).Result.ToList();
                var conflicts = shifts.Where(s => DoesShiftConflict(s, shift)).ToList();

                if (conflicts.Any() && !shift.OverrideAcknowleded)
                {
                    throw new ShiftConflictException()
                    {
                        Conflicts = conflicts
                    };
                }

                using (var scope = new TransactionScope())
                {                    
                    conflicts.ForEach(c => _residentRepo.Delete(c));
                    var response = _residentRepo.Save(shift);
                    if (!response.HasError)
                    {
                        shift = response.Result;
                    }
                    
                    scope.Complete();
                }
                shifts = _residentRepo.FindShiftsByResidentId(shift.InstitutionResidentId).Result.ToList();
                return new Tuple<ResidentShiftModel, IEnumerable<ResidentShiftModel>>(shift, shifts);
            };

            return Execute(func);
        }

        /// <summary>
        /// Method to evalute if an existing shift's times conflict with the unsaved one.
        /// </summary>
        /// <param name="existing"></param>
        /// <param name="unsaved"></param>
        /// <returns></returns>
        private bool DoesShiftConflict(ResidentShiftModel existing, ResidentShiftModel unsaved)
        {
            if(unsaved.Id == existing.Id) return false;

            if(unsaved.StartDateTime>= existing.StartDateTime && 
                (!existing.EndDateTime.HasValue || unsaved.StartDateTime <= existing.EndDateTime.Value))
            {
                return true;
            }

            if (unsaved.EndDateTime.HasValue && unsaved.EndDateTime.Value >= existing.StartDateTime &&
                existing.EndDateTime.HasValue && unsaved.EndDateTime.Value <= existing.EndDateTime.Value)
            {
                return true;
            }

            return false;
        }
    }
}

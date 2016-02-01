using DutyHours.Data.Repsitories;
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
        public ResponseModel<IEnumerable<ResidentShiftModel>> SaveShift(ResidentShiftModel shift)
        {
            Func<IEnumerable<ResidentShiftModel>> func = () =>
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
                    _residentRepo.Save(shift);
                }

                return _residentRepo.FindShiftsByResidentId(shift.InstitutionResidentId).Result;
            };

            return Execute<IEnumerable<ResidentShiftModel>>(func);
        }

        /// <summary>
        /// Method to evalute if an existing shift's times conflict with the unsaved one.
        /// </summary>
        /// <param name="existing"></param>
        /// <param name="unsaved"></param>
        /// <returns></returns>
        private bool DoesShiftConflict(ResidentShiftModel existing, ResidentShiftModel unsaved)
        {
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

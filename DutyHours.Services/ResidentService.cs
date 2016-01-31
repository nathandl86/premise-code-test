using DutyHours.Data.Repsitories;
using DutyHours.Models;
using DutyHours.Models.Data;
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
        public ResponseModel<IEnumerable<ResidentShift>> SaveShift(ResidentShift shift, bool overrideAcknowledged = false)
        {
            Func<IEnumerable<ResidentShift>> func = () =>
            {
                var shifts = _residentRepo.FindShiftsByResidentId(shift.InstitutionResident.UserId).Result.ToList();
                var conflicts = shifts.Where(s => DoesShiftConflict(s, shift)).ToList();

                if (conflicts.Any() && !overrideAcknowledged)
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

                return _residentRepo.FindShiftsByResidentId(shift.InstitutionResident.UserId).Result;
            };

            return Execute<IEnumerable<ResidentShift>>(func);
        }

        /// <summary>
        /// Method to evalute if an existing shift's times conflict with the unsaved one.
        /// </summary>
        /// <param name="existing"></param>
        /// <param name="unsaved"></param>
        /// <returns></returns>
        private bool DoesShiftConflict(ResidentShift existing, ResidentShift unsaved)
        {
            if(unsaved.StartDateTimeUtc >= existing.StartDateTimeUtc && 
                (!existing.EndDateTimeUtc.HasValue || unsaved.StartDateTimeUtc <= existing.EndDateTimeUtc.Value))
            {
                return true;
            }

            if (unsaved.EndDateTimeUtc.HasValue && unsaved.EndDateTimeUtc.Value >= existing.StartDateTimeUtc &&
                existing.EndDateTimeUtc.HasValue && unsaved.EndDateTimeUtc.Value <= existing.EndDateTimeUtc.Value)
            {
                return true;
            }

            return false;
        }
    }
}

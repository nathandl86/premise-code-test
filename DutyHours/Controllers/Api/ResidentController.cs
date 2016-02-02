using System.Web.Http;
using Mvc = System.Web.Mvc;
using Autofac.Integration.WebApi;
using System.Web.SessionState;
using System;
using DutyHours.Code;
using DutyHours.Models.Interfaces;
using DutyHours.Services;
using DutyHours.Models.Exceptions;
using DutyHours.Models;
using DutyHours.EntityData.Repsitories;

namespace DutyHours.Controllers.Api
{
    /// <summary>
    /// Api Controller for resident data lookups and persistence
    /// 
    /// Needs: Authorization restrictions on logged in users and around the ability
    /// to view and add shifts (either same logged in user or an admin)
    /// </summary>
    [RoutePrefix("api/resident")]
    [Mvc.SessionState(SessionStateBehavior.Disabled)]
    [AutofacControllerConfiguration]
    public class ResidentController : ApiController
    {
        private readonly IResidentRepository _residentRepo;
        private readonly IResidentService _residentSvc;
        private readonly ILogger _logger;

        public ResidentController(IResidentService residentSvc, IResidentRepository residentRepo, ILogger logger)
        {
            _residentSvc = residentSvc;
            _residentRepo = residentRepo;
            _logger = logger;
        }

        /// <summary>
        /// Api Method to retrieve a resident's user information
        /// </summary>
        /// <param name="residentId">Institution Resident id for the resident</param>
        /// <returns></returns>
        [Route("{residentId}")]
        [HttpGet]
        public IHttpActionResult GetResident(int residentId)
        {
            try
            {
                HttpRequires.IsTrue(residentId > 0, "A valid resident identifier is required");

                var response = _residentRepo.FindById(residentId);

                HttpAssert.Success(response);
                HttpAssert.NotNull(response, String.Format("Unable to find a resident with id [{0}]", residentId));

                return Ok(response.Result);
            }
            catch(Exception ex)
            {
                if(_logger != null)
                {
                    _logger.Write(ex);
                }
                return InternalServerError();
            }
        }

        /// <summary>
        /// Api Method to get a residents shifts
        /// </summary>
        /// <param name="residentId"></param>
        /// <returns></returns>
        [Route("{residentId}/shifts")]
        [HttpGet]
        public IHttpActionResult GetResidentShifts(int residentId)
        {
            try
            {
                HttpRequires.IsTrue(residentId > 0, "A valid resident identifier is required");

                var response = _residentRepo.FindShiftsByResidentId(residentId);

                HttpAssert.Success(response);
                HttpAssert.NotNull(response, String.Format("Unable to find shifts for resident with id [{0}]", residentId));

                return Ok(response.Result);
            }
            catch(Exception ex)
            {
                if(_logger != null)
                {
                    _logger.Write(ex);
                }
                return InternalServerError();
            }
        }

        /// <summary>
        /// Api method to save a residents shift. Will return a bad request with 
        /// conflicts if saving the data will result in overlapping dates with 
        /// pre-existing shifts. 
        /// </summary>
        /// <param name="overrideAck">User has the ability to acknowledge conflict
        ///        and choose to override them</param>
        /// <returns></returns>
        [Route("{residentId}/shift/save/{overrideAck?}")]
        [HttpPost]
        public IHttpActionResult SaveResidentShift(int residentId, [FromBody] ResidentShiftModel shift)
        {
            try
            {
                HttpRequires.IsNotNull(shift, "Shift Data Required");

                var response = _residentSvc.SaveShift(shift);

                HttpAssert.Success(response);

                // NOTE: THIS SECTION CONTAINS CHANGES ADDED AFTER THE 
                //    DEADLINE, TO RESOLVE BUG PERSISTING TIME ENTRIES
                return Ok(new
                { 
                    Shift = response.Result.Item1,
                    Shifts=response.Result.Item2
                });
            }
            catch(ShiftConflictException ex)
            {
                return this.BadRequest(new
                {
                    Type = "ShiftConflict",
                    Data = ex.Conflicts
                });
            }
            catch(Exception ex)
            {
                if(_logger != null)
                {
                    _logger.Write(ex);
                }
                return InternalServerError();
            }
        }
    }
}

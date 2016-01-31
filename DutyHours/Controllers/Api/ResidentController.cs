using System.Web.Http;
using Mvc = System.Web.Mvc;
using Autofac.Integration.WebApi;
using System.Web.SessionState;
using DutyHours.Data.Repsitories;
using System;
using DutyHours.Code;
using DutyHours.Models.Interfaces;
using DutyHours.Services;
using DutyHours.Models.Exceptions;
using DutyHours.Models.Data;

namespace DutyHours.Controllers.Api
{
    /// <summary>
    /// Api Controller for resident data lookups and persistence
    /// </summary>
    [RoutePrefix("api")]
    [Mvc.SessionState(SessionStateBehavior.Disabled)]
    [AutofacControllerConfiguration]
    public class ResidentController : ApiController
    {
        private readonly IResidentRepository _residentRepo;
        private readonly IResidentService _residentSvc;

        public ILogger Logger { get; set; }

        public ResidentController(IResidentService residentSvc, IResidentRepository residentRepo)
        {
            _residentSvc = residentSvc;
            _residentRepo = residentRepo;
        }

        /// <summary>
        /// Api Method to retrieve the residents within an institution
        /// </summary>
        [Route("institution/{institutionId}/residents")]
        [HttpGet]
        public IHttpActionResult GetInstitutionResidents(int institutionId)
        {
            try
            {
                HttpRequires.IsTrue(institutionId > 0, "A valid instituion identifier is required");

                var response = _residentRepo.FindByInstitutionId(institutionId);

                HttpAssert.Success(response);
                HttpAssert.NotNull(response, "Unable to find residents for the institution");

                return Ok(response.Result);
            }
            catch (Exception ex)
            {
                //TODO: Server side logging/notification here
                if(Logger != null)
                {
                    Logger.Write(ex);
                }
                return InternalServerError();
            }
        }

        /// <summary>
        /// Api Method to retrieve a resident's user information
        /// </summary>
        /// <param name="userId">User id for the resident</param>
        /// <returns></returns>
        [Route("resident/{userId}")]
        [HttpGet]
        public IHttpActionResult GetResident(int userId)
        {
            try
            {
                HttpRequires.IsTrue(userId > 0, "A valid user identifier is required");

                var response = _residentRepo.FindById(userId);

                HttpAssert.Success(response);
                HttpAssert.NotNull(response, String.Format("Unable to find a user with id [{0}]", userId));

                return Ok(response.Result);
            }
            catch(Exception ex)
            {
                if(Logger != null)
                {
                    Logger.Write(ex);
                }
                return InternalServerError();
            }
        }

        /// <summary>
        /// Api method to save a residents shift. Will return a bad request with 
        /// conflicts if saving the data will result in overlapping dates with 
        /// pre-existing shifts. 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [Route("resident/shift/save")]
        [HttpPost]
        public IHttpActionResult SaveResidentShift([FromBody] ResidentShift shift, [FromBody] bool overrideAck)
        {
            try
            {
                HttpRequires.IsNotNull(shift, "Shift Data Required");

                var response = _residentSvc.SaveShift(shift);

                HttpAssert.Success(response);
                return Ok();
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
                if(Logger != null)
                {
                    Logger.Write(ex);
                }
                return InternalServerError();
            }
        }
    }
}

using System;
using System.Web.Http;
using Mvc = System.Web.Mvc;
using Autofac.Integration.WebApi;
using System.Web.SessionState;
using DutyHours.Code;
using DutyHours.Data.Repsitories;
using DutyHours.Models.Interfaces;

namespace DutyHours.Controllers.Api
{
    [RoutePrefix("api/institution")]
    [Mvc.SessionState(SessionStateBehavior.Disabled)]
    [AutofacControllerConfiguration]
    public class InstitutionController : ApiController
    {
        private readonly IInstitutionRepository _institutionRepo;
        private readonly IResidentRepository _residentRepo;
        private readonly ILogger _logger;

        public InstitutionController(IInstitutionRepository institutionRepo, IResidentRepository residentRepo, ILogger logger)
        {
            _institutionRepo = institutionRepo;
            _residentRepo = residentRepo;
            _logger = logger;
        }

        /// <summary>
        /// Api Method to retrieve the residents within an institution
        /// </summary>
        [Route("{institutionId}/residents")]
        [HttpGet]
        public IHttpActionResult GetInstitutionResidents(int institutionId)
        {
            try
            {
                HttpRequires.IsTrue(institutionId > 0, "A valid instituion identifier is required");

                var response = _institutionRepo.FindResidentsByInstitutionId(institutionId);

                HttpAssert.Success(response);
                HttpAssert.NotNull(response, "Unable to find residents for the institution");

                return Ok(response.Result);
            }
            catch (Exception ex)
            {
                //TODO: Server side logging/notification here
                if (_logger != null)
                {
                    _logger.Write(ex);
                }
                return InternalServerError();
            }
        }

        /// <summary>
        /// Api method to get all institutions from the database.
        /// </summary>
        /// <returns></returns>
        [Route("all")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            try
            {
                var response = _institutionRepo.FindAll();

                HttpAssert.Success(response);
                HttpAssert.NotNull(response, "Unable to find institutions");

                return Ok(response.Result);
            }
            catch(Exception ex)
            {
                if (_logger != null) _logger.Write(ex);
                return InternalServerError();
            }
        }

        /// <summary>
        /// Api method to get an individual institution from the database.
        /// </summary>
        /// <param name="institutionId"></param>
        /// <returns></returns>
        [Route("{institutionId}")]
        [HttpGet]
        public IHttpActionResult Get(int institutionId)
        {
            try
            {
                HttpRequires.IsTrue(institutionId > 0, "A valid institution identifier is required");

                var response = _institutionRepo.Find(institutionId);

                HttpAssert.Success(response);
                HttpAssert.NotNull(response, String.Format("Unable to find an institution with id [{0}]", institutionId));

                return Ok(response.Result);
            }
            catch(Exception ex)
            {
                if (_logger != null) _logger.Write(ex);
                return InternalServerError();
            }
        }
    }
}

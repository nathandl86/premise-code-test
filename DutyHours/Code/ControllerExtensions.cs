using System.Net;
using System.Web.Http;
using System.Web.Http.Results;

namespace DutyHours.Code
{
    /// <summary>
    /// Extension methods for Api controllers
    /// </summary>
    public static class ControllerExtensions
    {
        /// <summary>
        /// Extension method on ApiController to allow for the BadRequest http code
        /// to be returned with data
        /// </summary>
        /// <typeparam name="T">Generic type for the data returned</typeparam>
        /// <param name="controller">ApiController</param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IHttpActionResult BadRequest<T>(this ApiController controller, T obj)
        {
            return new NegotiatedContentResult<T>(HttpStatusCode.BadRequest, obj, controller);
        }

    }
}
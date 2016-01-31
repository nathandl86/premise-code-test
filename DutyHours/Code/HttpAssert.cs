using DutyHours.Models;
using System;

namespace DutyHours.Code
{
    /// <summary>
    /// Class containing helper methods to validate data and service layer responses
    /// </summary>
    public class HttpAssert
    {
        /// <summary>
        /// Method to ensure that the response model returned is free of errors.
        /// </summary>
        /// <param name="model"></param>
        public static void Success(ResponseModel model)
        {
            if (model.HasError)
            {
                throw model.Error;
            }
        }

        /// <summary>
        /// Method to ensure that the result in the response model is not null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="msg"></param>
        public static void NotNull<T>(ResponseModel<T> model, string msg) where T : class
        {
            if (model.Result == null)
            {
                throw new NullReferenceException(msg);
            }
        }
    }
}
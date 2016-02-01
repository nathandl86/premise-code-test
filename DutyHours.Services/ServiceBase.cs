using DutyHours.Models;
using System;

namespace DutyHours.Services
{
    public class ServiceBase
    {
        /// <summary>
        /// Executes function with the specified return type. Internally handles exceptions
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        protected ResponseModel<T> Execute<T>(Func<T> func)
        {
            try
            {
                var result = func.Invoke();
                return new ResponseModel<T>
                {
                    Result = result,
                    HasError = false
                };
            }
            catch(Exception ex)
            {
                return new ResponseModel<T>
                {
                    Result = default(T),
                    HasError = true,
                    Error = ex
                };
            }
        }

        /// <summary>
        /// Executes a function without an expected return type. Internally handles exceptions
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        protected ResponseModel Execute(Action action)
        {
            try
            {
                action.Invoke();
                return new ResponseModel
                {
                    HasError = false
                };
            }
            catch(Exception ex)
            {
                return new ResponseModel
                {
                    HasError = true,
                    Error = ex
                };
            }
        }
    }
}

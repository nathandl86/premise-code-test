using DutyHours.Data.Mappers;
using DutyHours.Data.Models;
using DutyHours.Models;
using System;
using System.Collections.Generic;

namespace DutyHours.Data.Repsitories
{
    /// <summary>
    /// Base class for repositories using an Entity Framework datalayer.
    /// </summary>
    public abstract class EntityRepositoryBase
    {
        protected IDutyHoursDbContext DhDataContext { get; private set; }
        protected IMapper Mapper { get; private set; }

        /// <summary>
        /// Constructor with injected duty hours db context
        /// </summary>
        /// <param name="dhDbContext"></param>
        public EntityRepositoryBase(IDutyHoursDbContext dhDbContext, IMapper mapper) {
            DhDataContext = dhDbContext;
            Mapper = mapper;
        }

        /// <summary>
        /// Generic method to handle the persistence calls and to 
        /// map the results to the response model. Will also handle
        /// errors.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resolver"></param>
        /// <returns></returns>
        protected ResponseModel<T> Persist<T>(Func<T> resolver)
            where T : class
        {
            try
            {
                var entity = resolver.Invoke();
                return new ResponseModel<T>
                {
                    HasError = false,
                    Result = entity
                };
            }
            catch(Exception ex)
            {
                return new ResponseModel<T>
                {
                    HasError = true,
                    Error = ex,
                    Result = default(T)
                };
            }
        }

        /// <summary>
        /// Generic method to handle persistence calls when
        /// a return type isn't needed.
        /// </summary>
        /// <param name="resolver"></param>
        /// <returns></returns>
        protected ResponseModel Persist(Action resolver)
        {
            try
            {
                resolver.Invoke();
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

        /// <summary>
        /// Generic method to handle the retrieval of a single item.
        /// Results are mapped to the response object and errors are
        /// handled.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resolver"></param>
        /// <returns></returns>
        protected ResponseModel<T> Retrieve<T>(Func<T> resolver) 
            where T : class
        {
            try
            {
                var result = resolver.Invoke();
                return new ResponseModel<T>
                {
                    HasError = false,
                    Result = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<T>
                {
                    HasError = true,
                    Error = ex,
                    Result = default(T)
                };
            }
        }

        /// <summary>
        /// Generic method to return a collection of items. Results
        /// are mapped to the response object and errors are handled.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resolver"></param>
        /// <returns></returns>
        protected ResponseModel<IEnumerable<T>> RetrieveMany<T>(Func<IEnumerable<T>> resolver)
        {
            try
            {
                var result = resolver.Invoke();
                return new ResponseModel<IEnumerable<T>>
                {
                    HasError = false,
                    Result = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<IEnumerable<T>>
                {
                    HasError = true,
                    Error = ex,
                    Result = default(IEnumerable<T>)
                };
            }
        }
    }
}

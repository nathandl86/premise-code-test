using System;

namespace DutyHours.Models
{
    /// <summary>
    /// Generic model for returning a consistent, predictable type
    /// from Services and Repositories. Use this instance when
    /// you don't need a result.
    /// </summary>
    public class ResponseModel
    {
        public bool HasError { get; set; }
        public Exception Error { get; set; }
    }

    /// <summary>
    /// Generic model for returning a consistent, predictable type
    /// from Services and Repositories. Use this instance when
    /// you do need a result.
    /// </summary>
    public class ResponseModel<T> : ResponseModel
    {
        public T Result { get; set; }
    }
}

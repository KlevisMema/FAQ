#region Usinsg
using System.Net;
using Microsoft.AspNetCore.Identity;
#endregion

namespace FAQ.SHARED.ResponseTypes
{
    /// <summary>
    ///     A generic class which is used by register service and 
    ///     for more detailed response if an identity error happenns.
    ///     It inherits from <see cref="CommonResponse{}"/> and uses all it's properties.
    /// </summary>
    /// <typeparam name="T"> Any type of object </typeparam>
    public class IdentityResponse<T> : CommonResponse<T> where T : class
    {
        #region Properties

        /// <summary>
        ///     A <see cref="IEnumerable{T}"/> where T is <see cref="IdentityError"/>, collection
        ///     of identity errors. It is nullable.
        /// </summary>
        public IEnumerable<IdentityError>? IdentityErrors { get; set; }

        #endregion

        #region Constructor Overloadig 

        /// <summary>
        ///     Constructor.
        ///     Instasiate a new <see cref="IdentityResponse{}"/>  with all
        ///     props from the parent <see cref="CommonResponse{}"/> and <see cref="IdentityResponse{}"/> props.
        /// </summary>
        /// <param name="message"> Message <see cref="string"/> value, it's nullable </param>
        /// <param name="succsess"> Succsess <see cref="bool"/> value </param>
        /// <param name="statusCode"> Status code <see cref="HttpStatusCode"/> value </param>
        /// <param name="value"> Object <see cref="T"/> value, it's nullable </param>
        /// <param name="identityErrors"> Collection of <see cref="IdentityError"/> values, it's nullable </param>
        public IdentityResponse
        (
            string? message,
            bool succsess,
            HttpStatusCode statusCode,
            T? value,
            IEnumerable<IdentityError>? identityErrors
        )
        : base(message, succsess, statusCode, value)
        {
            Message = message;
            Succsess = succsess;
            StatusCode = statusCode;
            Value = value;
            IdentityErrors = identityErrors;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"> Message <see cref="string"/> value, it's nullable </param>
        /// <param name="succsess"> Succsess <see cref="bool"/> value </param>
        /// <param name="statusCode"> Status code <see cref="HttpStatusCode"/> value </param>
        /// <param name="Value"> Object <see cref="T"/> value, it's nullable </param>
        /// <param name="identityErrors"> Collection of <see cref="IdentityError"/> values, it's nullable </param>
        /// <returns> <see cref="IdentityResponse{T}"/> </returns>
        public static IdentityResponse<T> 
        Response
        (
            string? message,
            bool succsess,
            HttpStatusCode statusCode,
            T? Value,
            IEnumerable<IdentityError>? identityErrors
        )
        {
            return new IdentityResponse<T>(message, succsess, statusCode, Value, identityErrors);
        }

        #endregion
    }
}
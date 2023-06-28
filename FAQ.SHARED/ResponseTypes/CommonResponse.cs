#region Usings
using System.Net;
#endregion

namespace FAQ.SHARED.ResponseTypes
{
    /// <summary>
    ///     A generic class which is used by services and 
    ///     controlls for more detailed responses from services.
    /// </summary>
    /// <typeparam name="T"> Any type of object </typeparam>
    public class CommonResponse<T> where T : class
    {
        #region Properties

        /// <summary>
        ///     A <see cref="string"/> custom message describing an action succsess or fail  etc. 
        ///     It's nullable.
        /// </summary>
        public string? Message { get; set; }
        /// <summary>
        ///     A <see cref="bool"/>, indicating if an operation was succsess or not.
        /// </summary>
        public bool Succsess { get; set; }
        /// <summary>
        ///     A <see cref="HttpStatusCode"/>, indicatig the operation status code.
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }
        /// <summary>
        ///     A <see cref="T"/> obejct of any type.
        /// </summary>
        public T? Value { get; set; }

        #endregion

        #region Constructors overloading

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="message"> Message <see cref="string"/> value, it's nullable </param>
        /// <param name="succsess"> Succsess <see cref="bool"/> value </param>
        /// <param name="statusCode"> Status code <see cref="HttpStatusCode"/> value </param>
        /// <param name="value"> Object <see cref="T"/> value, it's nullable </param>
        public CommonResponse
        (
            string? message,
            bool succsess,
            HttpStatusCode statusCode,
            T? value
        )
        {
            Message = message;
            Succsess = succsess;
            StatusCode = statusCode;
            Value = value;
        }

        /// <summary>
        ///  A default constructor.
        /// </summary>
        public CommonResponse()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        ///     It creates a new CommonResponse using the costructor with all the fields.
        /// </summary>
        /// <param name="message"> Message <see cref="string"/> value, it's nullable </param>
        /// <param name="succsess"> Succsess <see cref="bool"/> value </param>
        /// <param name="statusCode"> Status code <see cref="HttpStatusCode"/> value </param>
        /// <param name="Value"> Object <see cref="T"/> value, it's nullable </param>
        /// <returns> <see cref="CommonResponse{T}"/> </returns>
        public static CommonResponse<T> 
        Response
        (
            string? message,
            bool succsess,
            HttpStatusCode statusCode,
            T? Value
        )
        {
            return new CommonResponse<T>(message, succsess, statusCode, Value);
        }

        #endregion

    }
}
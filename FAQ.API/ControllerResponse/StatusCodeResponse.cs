#region Usings
using System.Net;
using Azure;
using FAQ.SHARED.ResponseTypes;
using Microsoft.AspNetCore.Mvc;
#endregion

namespace FAQ.API.ControllerResponse
{
    /// <summary>
    ///     A class that provides statuss code response to be used in controllers.
    ///     Provides better code readablility.
    /// </summary>
    /// <typeparam name="T"> Any type of class </typeparam>
    public class StatusCodeResponse<T> : ControllerBase where T : class
    {
        #region Methods

        /// <summary>
        ///     Return a object result with <see cref="CommonResponse{T}"/> as object parameter depending from the service <see cref="HttpStatusCode"/> response.
        /// </summary>
        /// <param name="obj"> A <see cref="CommonResponse{T}"/> object </param>
        /// <returns> <see cref="ObjectResult"/> </returns>
        public static ObjectResult ControllerResponse(CommonResponse<T> obj)
        {
            return obj.StatusCode switch
            {
                HttpStatusCode.NotFound => new NotFoundObjectResult(obj),
                HttpStatusCode.BadRequest => new BadRequestObjectResult(obj),
                HttpStatusCode.OK => new OkObjectResult(obj),
                HttpStatusCode.Forbidden => new BadRequestObjectResult(obj),
                _ => new ObjectResult(obj) { StatusCode = StatusCodes.Status500InternalServerError },
            };
        }

        /// <param name="obj">List of object that will come from a controller</param>
        /// <returns>The appropriate status code</returns>
        public static ObjectResult ControllerResponseList(CommonResponse<List<T>> obj)
        {
            return obj.StatusCode switch
            {
                HttpStatusCode.NotFound => new NotFoundObjectResult(obj),
                HttpStatusCode.BadRequest => new BadRequestObjectResult(obj),
                HttpStatusCode.OK => new OkObjectResult(obj),
                HttpStatusCode.Forbidden => new BadRequestObjectResult(obj),
                _ => new ObjectResult(obj) { StatusCode = StatusCodes.Status500InternalServerError },
            };
        }

        #endregion
    }
}
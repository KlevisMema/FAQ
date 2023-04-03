using System.Net;
using Microsoft.AspNetCore.Identity;

namespace FAQ.SHARED.ResponseTypes
{
    public class IdentityResponse<T> : CommonResponse<T> where T : class
    {
        public IEnumerable<IdentityError>? IdentityErrors { get; set; }

        public IdentityResponse(string? message, bool succsess, HttpStatusCode statusCode, T? value, IEnumerable<IdentityError>? identityErrors) : base(message, succsess, statusCode, value)
        {
            Message = message;
            Succsess = succsess;
            StatusCode = statusCode;
            Value = value;
            IdentityErrors = identityErrors;
        }

        public static CommonResponse<T> Response(string? message, bool succsess, HttpStatusCode statusCode, T? Value, IEnumerable<IdentityError> identityErrors)
        {
            return new IdentityResponse<T>(message, succsess, statusCode, Value, identityErrors);
        }
    }
}
using System.Net;

namespace FAQ.SHARED.ResponseTypes
{
    public class CommonResponse<T> where T : class
    {
        public string? Message { get; set; }

        public bool Succsess { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public T? Value { get; set; }

        public CommonResponse(string? message, bool succsess, HttpStatusCode statusCode, T? value)
        {
            Message = message;
            Succsess = succsess;
            StatusCode = statusCode;
            Value = value;
        }

        public static CommonResponse<T> Response(string? message, bool succsess, HttpStatusCode statusCode, T? Value)
        {
            return new CommonResponse<T>(message, succsess, statusCode, Value);
        }

    }
}
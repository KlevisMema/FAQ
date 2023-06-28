#region Usings
using FAQ.SECURITY.ApplicationAuthorizationService.ServiceInterface;
using FAQ.SHARED.ResponseTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
#endregion

namespace FAQ.SECURITY.ApplicationAuthorizationService.ServiceImplementation
{
    /// <summary>
    ///     A Custom Filter to be used by the controllers by implementing <see cref="IAuthorizationFilter"/>
    ///     and <see cref="IApiKeyAuthorizationFilter"/> to provide the abstraction and DI.
    ///     This filter duty is to guard the API in application level.
    /// </summary>
    public class ApiKeyAuthorizationFilter : IAuthorizationFilter, IApiKeyAuthorizationFilter
    {
        #region Fields and Constructor
        /// <summary>
        ///     A private <see cref="string"/> that will hold the value of the api key value.
        /// </summary>
        private readonly string _apiKey;

        /// <summary>
        ///     Instansiate a new <see cref="ApiKeyAuthorizationFilter"/>.
        ///     Requires a string that it should be the api key from appsettings.json
        /// </summary>
        /// <param name="apiKey"> The <see cref="string"/> api key </param>
        public ApiKeyAuthorizationFilter
        (
            string apiKey
        )
        {
            _apiKey = apiKey;
        }
        #endregion

        #region Implementation

        public void 
        OnAuthorization
        (
            AuthorizationFilterContext context
        )
        {
            string? apiKey = context.HttpContext.Request.Headers["X-Api-Key"].FirstOrDefault();

            if (String.IsNullOrEmpty(apiKey))
                context.Result = new UnauthorizedResult();

            if (apiKey != _apiKey)
                context.Result = new UnauthorizedResult();
        }

        #endregion
    }
}
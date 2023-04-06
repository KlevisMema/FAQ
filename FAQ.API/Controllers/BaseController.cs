#region Usings
using Microsoft.AspNetCore.Mvc;
using FAQ.SECURITY.ApplicationAuthorizationService.ServiceInterface;
#endregion

namespace FAQ.API.Controllers
{
    /// <summary>
    ///     A Base Api controller that inherits from  <see cref="ControllerBase"/> and 
    ///     it's inherited by all controllers. This controller is configured that the route is : 
    ///     api/<see langword="[controller]"/> were <see langword="[controller]"/> is the controller name this by adding <see cref="RouteAttribute"/>.
    ///     A filter <see cref="ServiceFilterAttribute"/> is added to protect all the controllers by being accessed by everyone in application level. 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [ServiceFilter(typeof(IApiKeyAuthorizationFilter))]
    public class BaseController : ControllerBase
    {
    }
}
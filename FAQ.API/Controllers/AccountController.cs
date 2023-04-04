#region Usings
using FAQ.SHARED.ResponseTypes;
using Microsoft.AspNetCore.Mvc;
using FAQ.API.ControllerResponse;
using Microsoft.AspNetCore.Authorization;
using FAQ.ACCOUNT.AccountService.ServiceInterface;
#endregion

namespace FAQ.API.Controllers
{
    /// <summary>
    ///     A controller providing endpoints for user functionalities.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        #region Services Injection

        /// <summary>
        ///     A  <see langword="private"/> readonly field for <see cref="IAccountService"/>
        /// </summary>
        private readonly IAccountService _accountService;

        /// <summary>
        ///     Inject services in constructor
        /// </summary>
        /// <param name="accountService"> Account service </param>
        public AccountController
        (
            IAccountService accountService
        )
        {
            _accountService = accountService;
        }

        #endregion

        #region Endpoints

        /// <summary>
        ///     Confirm email of a user endpoint.
        ///     This endpoint is accessed by everyone by marking it with : <see cref="AllowAnonymousAttribute"/>.
        ///     Its a post endpoint marked with : <see cref="HttpPostAttribute"/>.
        /// </summary>
        /// <param name="userId"> 
        ///     Id of the user value of type <see cref="Guid"/>,
        ///     this param should be send from route, its marked with : <see cref="FromRouteAttribute"/>
        /// </param>
        /// <param name="otp"> One time password value of type <see cref="string"/> </param>
        /// <returns> <see cref="Task"/> of <see cref="CommonResponse{T}"/> where T is  <see cref="string"/> </returns>
        [AllowAnonymous]
        [HttpPost("ConfirmEmail/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommonResponse<string>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CommonResponse<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CommonResponse<string>))]
        public async Task<ActionResult<CommonResponse<string>>> ConfrimEmail
        (
            [FromRoute] Guid userId,
            string otp
        )
        {
            var confirmEmailResult = await _accountService.ConfirmEmail(userId.ToString(), otp);

            return StatusCodeResponse<string>.ControllerResponse(confirmEmailResult);
        }

        #endregion
    }
}

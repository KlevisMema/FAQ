#region Usings
using FAQ.SHARED.ResponseTypes;
using Microsoft.AspNetCore.Mvc;
using FAQ.API.ControllerResponse;
using Microsoft.AspNetCore.Authorization;
using FAQ.ACCOUNT.AccountService.ServiceInterface;
using FAQ.DTO.UserDtos;
using Microsoft.Extensions.Options;
using System.Net;
#endregion

namespace FAQ.API.Controllers
{
    /// <summary>
    ///     A controller providing endpoints for user functionalities.
    /// </summary>
    public class AccountController : BaseController
    {
        #region Services Injection

        /// <summary>
        ///     A  <see langword="private"/> readonly field for <see cref="IAccountService"/>
        /// </summary>
        private readonly IAccountService _accountService;
        /// <summary>
        ///     A  <see langword="private"/> readonly field for <see cref="IWebHostEnvironment"/>
        /// </summary>
        private readonly IWebHostEnvironment _webHostEnvironment;

        /// <summary>
        ///     Inject services in constructor
        /// </summary>
        /// <param name="accountService"> Account service </param>
        /// <param name="webHostEnvironment"> Web Enviroment </param>
        public AccountController
        (
            IAccountService accountService,
            IWebHostEnvironment webHostEnvironment
        )
        {
            _accountService = accountService;
            _webHostEnvironment = webHostEnvironment;
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
        /// <returns> 
        ///     <see cref="ActionResult{TValue}"/> where TValue is <see cref="CommonResponse{T}"/> and T is  <see cref="string"/> 
        /// </returns>
        [AllowAnonymous]
        [HttpPost("ConfirmEmail/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommonResponse<string>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CommonResponse<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CommonResponse<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CommonResponse<string>))]
        public async Task<ActionResult<CommonResponse<string>>> ConfrimEmail
        (
            [FromRoute] Guid userId,
            string otp
        )
        {
            return StatusCodeResponse<string>.ControllerResponse(await _accountService.ConfirmEmail(userId.ToString(), otp));
        }

        /// <summary>
        ///     Upload a profile picture for a user endpoint. 
        ///     This endpoint is accessed by everyone by marking it with : <see cref="AllowAnonymousAttribute"/>.
        ///     Its a post endpoint marked with : <see cref="HttpPostAttribute"/>.
        /// </summary>
        /// <param name="userId"> 
        ///     <see cref="Guid"/> ID of the user.
        ///     This param should be send from route, its marked with : <see cref="FromRouteAttribute"/>
        /// </param>
        /// <param name="picUpload">
        ///     <see cref="DtoProfilePicUpload"/> object.
        ///      This param should be send from form, its marked with : <see cref="FromFormAttribute"/>
        /// </param>
        /// <returns> 
        ///     <see cref="ActionResult{TValue}"/> where TValue <see cref="CommonResponse{T}"/>
        ///     where T is <see cref="DtoProfilePicUpload"/>.
        /// </returns>
        [AllowAnonymous]
        [HttpPost("UploadProfilePricture/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommonResponse<DtoProfilePicUpload>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CommonResponse<DtoProfilePicUpload>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CommonResponse<DtoProfilePicUpload>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CommonResponse<DtoProfilePicUpload>))]
        public async Task<ActionResult<CommonResponse<DtoProfilePicUpload>>> UploadProfilePicture
        (
            [FromRoute] Guid userId,
            [FromForm] DtoProfilePicUpload picUpload
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return StatusCodeResponse<DtoProfilePicUpload>.ControllerResponse(await _accountService.UploadProfilePicture(userId, picUpload, _webHostEnvironment));

        }

        #endregion
    }
}
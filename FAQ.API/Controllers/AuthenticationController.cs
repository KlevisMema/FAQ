#region Usings

using FAQ.DTO.UserDtos;
using FAQ.SHARED.ResponseTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FAQ.ACCOUNT.AuthorizationService.Interfaces;
using FAQ.API.ControllerResponse;
using FAQ.SECURITY.ApplicationAuthorizationService.ServiceImplementation;
using FAQ.SECURITY.ApplicationAuthorizationService.ServiceInterface;

#endregion

namespace FAQ.API.Controllers
{
    /// <summary>
    ///     A authentication contoller providing endpoinnts for login and logout.
    /// </summary>
    public class AuthenticationController : BaseController
    {
        #region Services Injection
        /// <summary>
        ///     A <see langword="private"/> <see langword="readonly"/> field for <seealso cref="ILoginService"/>.
        /// </summary>
        private readonly ILoginService _loginService;
        /// <summary>
        ///     A <see langword="private"/> <see langword="readonly"/> field for <seealso cref="IRegisterService"/>.
        /// </summary>
        private readonly IRegisterService _registerService;

        /// <summary>
        ///     Constructor, register <see cref="ILoginService"/> and <see cref="IRegisterService"/> 
        /// </summary>
        /// <param name="loginService"> <see cref="ILoginService"/> </param>
        /// <param name="registerService"> <see cref="IRegisterService"/> </param>
        public AuthenticationController
        (
            ILoginService loginService,
            IRegisterService registerService
        )
        {
            _loginService = loginService;
            _registerService = registerService;
        }

        #endregion

        #region Register Endpoint

        /// <summary>
        ///     Register user endpoint.
        ///     This endpoint is accessed by everyone by marking it with : <see cref="AllowAnonymousAttribute"/>.
        ///     This is a post endpoint marked with : <see cref="HttpPostAttribute"/>.
        /// </summary>
        /// <param name="register"> 
        ///     Register data object value of type <see cref="DtoRegister"/>,  
        ///     this param should be send from a form its marked with : <see cref="FromFormAttribute"/>. 
        /// </param>
        /// <returns> <see cref="Task"/> of <see cref="CommonResponse{T}"/> where T is  <see cref="DtoRegister"/> </returns>
        [AllowAnonymous]
        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommonResponse<DtoRegister>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CommonResponse<DtoRegister>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CommonResponse<DtoRegister>))]
        public async Task<ActionResult<CommonResponse<DtoRegister>>> Register
        (
            [FromForm] DtoRegister register
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resultRegister = await _registerService.Register(register);

            return StatusCodeResponse<DtoRegister>.ControllerResponse(resultRegister);
        }

        #endregion

        #region Login Endpoint

        /// <summary>
        ///     Login user endpoint.
        ///     This endpoint is accessed by everyone by marking it with : <see cref="AllowAnonymousAttribute"/>.
        ///     This is a post marked with : <see cref="HttpPostAttribute"/>.
        /// </summary>
        /// <param name="logIn">
        ///     Login data object value of type <see cref="DtoLogin"/>,
        ///     this parameter value should be send from a from its marked with : <see cref="FromFormAttribute"/>
        /// </param>
        /// <returns> <see cref="Task"/> of <see cref="CommonResponse{T}"/> where T is  <see cref="DtoLogin"/> </returns>
        [AllowAnonymous]
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommonResponse<DtoLogin>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CommonResponse<DtoLogin>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CommonResponse<DtoLogin>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CommonResponse<DtoLogin>))]
        public async Task<ActionResult<CommonResponse<DtoLogin>>> LogIn
        (
            [FromForm] DtoLogin logIn
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resultLogin = await _loginService.Login(logIn);

            return StatusCodeResponse<DtoLogin>.ControllerResponse(resultLogin);
        }

        #endregion
    }
}
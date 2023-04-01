#region Usings

using FAQ.DTO.UserDtos;
using FAQ.SHARED.ResponseTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FAQ.ACCOUNT.AuthorizationService.Interfaces;

#endregion

namespace FAQ.API.Controllers
{
    /// <summary>
    ///     A authentication contoller for login and logout
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        #region Services Injection

        private readonly ILoginService _loginService;
        private readonly IRegisterService _registerService;

        /// <summary>
        ///     Register ILoginService and IRegisterService
        /// </summary>
        /// <param name="loginService"></param>
        /// <param name="registerService"></param>
        public AuthenticationController(ILoginService loginService, IRegisterService registerService)
        {
            _loginService = loginService;
            _registerService = registerService;
        }

        #endregion

        #region Register Endpoint

        /// <summary>
        ///     Register a user 
        /// </summary>
        /// <param name="register">Register data object</param>
        /// <param name="cancellationToken"> Cancellation token </param>

        [AllowAnonymous]
        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public async Task<ActionResult<CommonResponse<DtoRegister>>> Register
        (
            [FromForm] DtoRegister register,
            CancellationToken cancellationToken
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _registerService.Register(register);

            return result;

        }

        #endregion

        #region Login Endpoint

        /// <summary>
        ///     Login a user
        /// </summary>
        /// <param name="logIn"> Login data object </param>

        [AllowAnonymous]
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public async Task<ActionResult<CommonResponse<DtoLogin>>> LogIn
        (
            [FromForm] DtoLogin logIn
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _loginService.Login(logIn);

            return result;
        }

        #endregion
    }
}
using FAQ.DTO.UserDtos;
using FAQ.SERVICES.AuthorizationService.Interfaces;
using FAQ.SHARED.ResponseTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FAQ.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IRegisterService _registerService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginService"></param>
        /// <param name="registerService"></param>
        public AuthenticationController(ILoginService loginService, IRegisterService registerService)
        {
            _loginService = loginService;
            _registerService = registerService;
        }

        #region Register 

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
        public async Task<ActionResult<CommonResponse<RegisterViewModel>>> Register
        (
            [FromForm] RegisterViewModel register,
            CancellationToken cancellationToken
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _registerService.Register(register);

            return result;

        }

        #endregion

        /// <summary>
        ///     Login a user
        /// </summary>
        /// <param name="logIn"> Login data object </param>
        /// <param name="cancellationToken"> Cancellation token </param>

        [AllowAnonymous]
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public async Task<ActionResult<CommonResponse<LoginViewModel>>> LogIn
        (
            [FromForm] LoginViewModel logIn,
            CancellationToken cancellationToken
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _loginService.Login(logIn);

            return result;
        }
    }
}

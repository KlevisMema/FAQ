#region Usings
using AutoMapper;
using FAQ.DAL.Models;
using FAQ.DTO.UserDtos;
using FAQ.SHARED.ResponseTypes;
using FAQ.LOGGER.ServiceInterface;
using Microsoft.AspNetCore.Identity;
using FAQ.ACCOUNT.AuthorizationService.Interfaces;
using FAQ.ACCOUNT.AuthenticationService.ServiceInterface;
using FAQ.SHARED.ServicesMessageResponse;
using Microsoft.Extensions.Options;
using FAQ.EMAIL.EmailService.ServiceInterface;
#endregion

namespace FAQ.ACCOUNT.AuthorizationService.Implementation
{
    /// <summary>
    ///     A service class providing the login functionality by implementing <see cref="ILoginService"/> interface
    /// </summary>
    public class LoginService : ILoginService
    {
        #region Services Injection
        /// <summary>
        ///   A readonly field for  Auth interface 
        /// </summary>
        private readonly IOAuthJwtTokenService _oAuthService;
        /// <summary>
        ///   A readonly field for  User Manager where UserManager is type of <see cref="UserManager{TUser}"/> where TUser is <see cref="User"/>
        /// </summary>
        private readonly UserManager<User> _userManager;
        /// <summary>
        ///    A readonly field for Sign in manager where SignInManager is type of <see cref="SignInManager{TUser}"/> where TUser is <see cref="User"/>
        /// </summary>
        private readonly SignInManager<User> _signInManager;
        /// <summary>
        ///     Log service
        /// </summary>
        private readonly ILogService _log;
        /// <summary>
        ///     Message response register 
        /// </summary>
        private LogInMessageResponse _logInMessageResponse { get; set; } = new();
        /// <summary>
        ///     Exception message response
        /// </summary>
        private string ExceptionMessageResponse { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private readonly IEmailSender _emailSender;

        /// <summary>
        ///     Inject all services in constructor
        /// </summary>
        /// <param name="oAuthService"> OAuth service </param>
        /// <param name="userManager"> User Manager service </param>
        /// <param name="signInManager"> Sign In service </param>
        /// <param name="log"> Logger service </param>
        /// <param name="messageResponses"> Message response </param>
        public LoginService
        (
            ILogService log,
            IEmailSender emailSender,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IOAuthJwtTokenService oAuthService,
            IOptions<ServiceMessageResponseContainer> messageResponses
        )
        {
            _log = log;
            _userManager = userManager;
            _emailSender = emailSender;
            _oAuthService = oAuthService;
            _signInManager = signInManager;
            _logInMessageResponse = messageResponses.Value.LogInMessageResponse!;
            ExceptionMessageResponse = messageResponses.Value.Exception;
        }
        #endregion

        #region Method implementation

        /// <summary>
        ///     Log in a user and genereate a token method implementation.
        /// </summary>
        /// <param name="logIn"> Login object of type <see cref="DtoLogin"/> </param>
        /// <returns> A object response of <see cref="CommonResponse{DtoLogin}"/> where <see langword="T"/> is <seealso cref="DtoLogin"/> </returns>
        public async Task<CommonResponse<DtoLogin>> 
        Login
        (
            DtoLogin logIn
        )
        {
            string UserId = "";

            try
            {
                var user = await _userManager.FindByEmailAsync(logIn.Email);

                if (user is null)
                    return CommonResponse<DtoLogin>.Response(_logInMessageResponse.UserNotFound, false, System.Net.HttpStatusCode.NotFound, null);

                var emailConfirmed = await _userManager.IsEmailConfirmedAsync(user!);

                if (!emailConfirmed)
                    return CommonResponse<DtoLogin>.Response(_logInMessageResponse.UnconfirmedEmail, false, System.Net.HttpStatusCode.NotFound, logIn);

                var result = await _signInManager.PasswordSignInAsync(logIn.Email, logIn.Password, false, false);

                if (result.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles.Count == 0)
                        return CommonResponse<DtoLogin>.Response(_logInMessageResponse.UserNoRoles, false, System.Net.HttpStatusCode.NotFound, logIn);

                    var userTransformedObj = new DtoUser()
                    {
                        Id = user!.Id,
                        Email = logIn.Email,
                        Roles = roles.ToList(),
                    };

                    UserId = user.Id;

                    return CommonResponse<DtoLogin>.Response($"{_oAuthService.CreateToken(userTransformedObj)}", true, System.Net.HttpStatusCode.OK, logIn);
                }

                return CommonResponse<DtoLogin>.Response(_logInMessageResponse.InvalidCredentials, false, System.Net.HttpStatusCode.BadRequest, logIn);
            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "Log In", null);

                await _emailSender.SendEmailToDevTeam(Guid.Parse(UserId));

                return CommonResponse<DtoLogin>.Response(ExceptionMessageResponse, false, System.Net.HttpStatusCode.InternalServerError, null);
            }
        }

        #endregion

    }
}
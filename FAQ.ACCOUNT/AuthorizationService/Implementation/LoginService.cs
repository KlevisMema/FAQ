#region Usings
using AutoMapper;
using FAQ.DAL.Models;
using FAQ.DTO.UserDtos;
using FAQ.SHARED.ResponseTypes;
using Microsoft.AspNetCore.Identity;
using FAQ.SERVICES.RepositoryService.Interfaces;
using FAQ.ACCOUNT.AuthorizationService.Interfaces;
using FAQ.ACCOUNT.AuthenticationService.ServiceInterface;
#endregion

namespace FAQ.ACCOUNT.AuthorizationService.Implementation
{
    public class LoginService : ILoginService
    {
        #region Services Injection
        /// <summary>
        ///    A readonly field for Mapper service
        /// </summary>
        private readonly IMapper _mapper;
        /// <summary>
        ///   A readonly field for  Auth interface 
        /// </summary>
        private readonly IOAuthJwtTokenService _oAuthService;
        /// <summary>
        ///   A readonly field for  User Manager
        /// </summary>
        private readonly UserManager<User> _userManager;
        /// <summary>
        ///    A readonly field for Sign in manager
        /// </summary>
        private readonly SignInManager<User> _signInManager;
        /// <summary>
        ///     Log service
        /// </summary>
        private readonly ILogService _log;

        /// <summary>
        ///     Inject all services in constructor
        /// </summary>
        /// <param name="oAuthService"> OAuth service </param>
        /// <param name="userManager"> User Manager service </param>
        /// <param name="signInManager"> Sign In service </param>
        /// <param name="db"> Log service </param>
        public LoginService
        (
            IMapper mapper,
            ILogService log,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IOAuthJwtTokenService oAuthService
        )
        {
            _log = log;
            _mapper = mapper;
            _userManager = userManager;
            _oAuthService = oAuthService;
            _signInManager = signInManager;
        }
        #endregion

        #region Method

        /// <summary>
        ///     Log in a user and genereate a token
        /// </summary>
        /// <param name="logIn"> Login object </param>

        public async Task<CommonResponse<DtoLogin>> Login
        (
            DtoLogin logIn
        )
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(logIn.Email);

                if (user is null)
                    return CommonResponse<DtoLogin>.Response("User doesn't exists !!", false, System.Net.HttpStatusCode.NotFound, logIn);

                var emailConfirmed = await _userManager.IsEmailConfirmedAsync(user!);

                if (!emailConfirmed)
                    return CommonResponse<DtoLogin>.Response("You haven't confirmed you email yet!", false, System.Net.HttpStatusCode.NotFound, logIn);

                var result = await _signInManager.PasswordSignInAsync(logIn.Email, logIn.Password, false, false);

                if (result.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles.Count == 0)
                        return CommonResponse<DtoLogin>.Response("User doesn't have any role !!", false, System.Net.HttpStatusCode.NotFound, logIn);

                    var userTransformedObj = new DtoUser()
                    {
                        Id = user!.Id,
                        Email = logIn.Email,
                        Roles = roles.ToList(),
                    };

                    return CommonResponse<DtoLogin>.Response($"{_oAuthService.CreateToken(userTransformedObj)}", true, System.Net.HttpStatusCode.OK, logIn);
                }

                return CommonResponse<DtoLogin>.Response("Invalid credentials", false, System.Net.HttpStatusCode.BadRequest, logIn);
            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "Log In", null);

                return CommonResponse<DtoLogin>.Response("Iternal server error", false, System.Net.HttpStatusCode.InternalServerError, new DtoLogin());
            }
        }

        #endregion

    }
}
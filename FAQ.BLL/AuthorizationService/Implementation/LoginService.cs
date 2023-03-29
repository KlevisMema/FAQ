#region Usings
using AutoMapper;
using FAQ.DAL.Models;
using FAQ.DTO.UserDtos;
using FAQ.SHARED.ResponseTypes;
using Microsoft.AspNetCore.Identity;
using FAQ.SERVICES.AuthorizationService.Interfaces;
using FAQ.SERVICES.AuthenticationService.ServiceInterface;
using FAQ.DAL.DataBase;
#endregion

namespace FAQ.SERVICES.AuthorizationService.Implementation
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
        ///     Database context
        /// </summary>
        private readonly ApplicationDbContext _db;

        /// <summary>
        ///     Inject all services in constructor
        /// </summary>
        /// <param name="oAuthService"> OAuth service </param>
        /// <param name="userManager"> User Manager service </param>
        /// <param name="signInManager"> Sign In service </param>
        public LoginService
        (
            IOAuthJwtTokenService oAuthService,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IMapper mapper
,
            ApplicationDbContext db)
        {
            _userManager = userManager;
            _oAuthService = oAuthService;
            _signInManager = signInManager;
            _mapper = mapper;
            _db = db;
        }
        #endregion

        #region Method

        /// <summary>
        ///     Log in a user and genereate a token
        /// </summary>
        /// <param name="logIn"> Login object </param>

        public async Task<CommonResponse<LoginViewModel>> Login
        (
            LoginViewModel logIn
        )
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(logIn.Email, logIn.Password, false, false);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(logIn.Email);

                    if (user == null)
                        CommonResponse<LoginViewModel>.Response("User doesn't exists !!", false, System.Net.HttpStatusCode.NotFound, new LoginViewModel());

                    var roles = await _userManager.GetRolesAsync(_mapper.Map<User>(logIn));

                    if (roles.Count == 0)
                        CommonResponse<LoginViewModel>.Response("User doesn't have any role !!", false, System.Net.HttpStatusCode.NotFound, new LoginViewModel());

                    var userTransformedObj = new UserViewModel()
                    {
                        Id = user!.Id,
                        Email = logIn.Email,
                        Roles = roles.ToList(),
                    };

                    return CommonResponse<LoginViewModel>.Response($"{_oAuthService.CreateToken(userTransformedObj)}", true, System.Net.HttpStatusCode.OK, logIn);
                }

                return CommonResponse<LoginViewModel>.Response("User login attempt failed", false, System.Net.HttpStatusCode.BadRequest, new LoginViewModel());
            }
            catch (Exception ex)
            {
                return CommonResponse<LoginViewModel>.Response("Iternal server error", false, System.Net.HttpStatusCode.InternalServerError, new LoginViewModel());
            }
        }

        #endregion

    }
}
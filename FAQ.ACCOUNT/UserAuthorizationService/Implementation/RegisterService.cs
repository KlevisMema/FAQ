#region Usings
using AutoMapper;
using FAQ.DAL.Models;
using FAQ.DAL.DataBase;
using FAQ.DTO.UserDtos;
using FAQ.SHARED.ResponseTypes;
using FAQ.LOGGER.ServiceInterface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using FAQ.EMAIL.EmailService.ServiceInterface;
using FAQ.ACCOUNT.AuthorizationService.Interfaces;
#endregion

namespace FAQ.ACCOUNT.AuthorizationService.Implementation
{
    /// <summary>
    ///     A service class providing the registration by implementing <see cref="IRegisterService"/> interface.
    /// </summary>
    public class RegisterService : IRegisterService
    {
        #region Services Injection

        /// <summary>
        ///    A readonly field for Mapper service
        /// </summary>
        private readonly IMapper _mapper;
        /// <summary>
        ///   A readonly field for User Manager
        /// </summary>
        private readonly UserManager<User> _userManager;
        /// <summary>
        ///     Database Context
        /// </summary>
        private readonly ApplicationDbContext _db;
        /// <summary>
        ///     Log service
        /// </summary>
        private readonly ILogService _log;
        /// <summary>
        ///     Email sender service 
        /// </summary>
        private readonly IEmailSender _emailSender;

        /// <summary>
        ///     Inject all services in constructor
        /// </summary>
        /// <param name="userManager"> User Manager service </param>
        /// <param name="mapper"> Auto mapper </param>
        /// <param name="log"> Log service </param>
        /// <param name="db"> Database context </param>
        /// <param name="emailSender"> Email sender </param>
        public RegisterService
        (
            IMapper mapper,
            ILogService log,
            ApplicationDbContext db,
            IEmailSender emailSender,
            UserManager<User> userManager
        )
        {
            _db = db;
            _log = log;
            _mapper = mapper;
            _emailSender = emailSender;
            _userManager = userManager;
        }

        #endregion

        #region Method implementation

        /// <summary>
        ///     Register a user method implementation
        /// </summary>
        /// <param name="register"> Dto register object </param>
        /// <returns> A  object  response of <see cref="CommonResponse{T}"/>' where <see langword="T"/> is <seealso cref="DtoRegister"/> </returns>
        public async Task<CommonResponse<DtoRegister>> Register
        (
           DtoRegister register
        )
        {
            try
            {
                var user = _mapper.Map<User>(register);

                var otp = GenerateOTP();

                user.OTP = otp;

                var result = await _userManager.CreateAsync(user, register.Password);

                if (result.Succeeded)
                {
                    var registeredUser = await _userManager.FindByEmailAsync(register.Email);

                    var role = await _db.Roles.FirstOrDefaultAsync(x => x.Name!.Equals("User"));

                    if (role is not null)
                        await _userManager.AddToRoleAsync(user, role.Name!);

                    await _emailSender.SendConfirmEmail(new DtoUserConfirmEmail { Email = register.Email, UserId = Guid.Parse(user.Id) }, otp);

                    if (registeredUser is not null)
                        await _log.CreateLogAction($"[Succsess] - A user just registered at {DateTime.Now} with the email {registeredUser.Email}", "Register", Guid.Parse(registeredUser.Id));

                    return CommonResponse<DtoRegister>.Response($"Register succsessful", true, System.Net.HttpStatusCode.OK, register);
                }

                return IdentityResponse<DtoRegister>.Response($"User registration attempt failed", false, System.Net.HttpStatusCode.BadRequest, register, result.Errors);
            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "Register", null);

                return CommonResponse<DtoRegister>.Response("Internal server error", false, System.Net.HttpStatusCode.InternalServerError, register);
            }
        }

        /// <summary>
        ///     Generate a unique guid and take only the first part 
        /// </summary>
        /// <returns> The newly generated code </returns>
        private static string GenerateOTP
        (
        )
        {
            var otp = Guid.NewGuid().ToString().Substring(0, 7);

            return otp;
        }

        #endregion
    }
}
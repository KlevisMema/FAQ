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
using Microsoft.Extensions.Options;
using FAQ.SHARED.ServicesMessageResponse;
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
        ///     Message response register 
        /// </summary>
        private RegisterMessageResponse registerMessageResponse { get; set; } = new();
        /// <summary>
        ///     Exception message response
        /// </summary>
        private string ExceptionMessageResponse { get; set; }

        /// <summary>
        ///     Inject all services in constructor
        /// </summary>
        /// <param name="userManager"> User Manager service </param>
        /// <param name="mapper"> Auto mapper </param>
        /// <param name="log"> Log service </param>
        /// <param name="db"> Database context </param>
        /// <param name="emailSender"> Email sender </param>
        /// <param name="messageResponses"> Message response </param>
        public RegisterService
        (
            IMapper mapper,
            ILogService log,
            ApplicationDbContext db,
            IEmailSender emailSender,
            UserManager<User> userManager,
            IOptions<ServiceMessageResponseContainer> messageResponses
        )
        {
            _db = db;
            _log = log;
            _mapper = mapper;
            _emailSender = emailSender;
            _userManager = userManager;
            registerMessageResponse = messageResponses.Value.RegisterMessageResponse!;
            ExceptionMessageResponse = messageResponses.Value.Exception;
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
            string UserId = "";

            try
            {
                var user = _mapper.Map<User>(register);

                var otp = GenerateOTP();

                user.OTP = otp;

                var result = await _userManager.CreateAsync(user, register.Password);

                if (result.Succeeded)
                {
                    var registeredUser = await _userManager.FindByEmailAsync(register.Email);

                    UserId = registeredUser!.Id;

                    var role = await _db.Roles.FirstOrDefaultAsync(x => x.Name!.Equals("User"));

                    if (role is not null)
                        await _userManager.AddToRoleAsync(user, role.Name!);

                    await _emailSender.SendConfirmEmail(new DtoUserConfirmEmail { Email = register.Email, UserId = Guid.Parse(user.Id) }, otp);

                    if (registeredUser is not null)
                        await _log.CreateLogAction
                        (
                            registerMessageResponse.SaveSuccsessRegistrationLog
                                .Replace("{DateTime.Now}", DateTime.Now.ToString())
                                .Replace("{registeredUser.Email}", registeredUser!.Email),
                            "Register",
                            Guid.Parse(registeredUser.Id)
                        );

                    await _log.CreateLogAction($"User with id {registeredUser!.Id} created an was just created", "Register", Guid.Parse(registeredUser!.Id));

                    return CommonResponse<DtoRegister>.Response(registerMessageResponse.SuccsessRegistration, true, System.Net.HttpStatusCode.OK, register);
                }

                return IdentityResponse<DtoRegister>.Response(registerMessageResponse.FailRegistration, false, System.Net.HttpStatusCode.BadRequest, register, result.Errors);
            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "Register", null);

                await _emailSender.SendEmailToDevTeam(Guid.Parse(UserId));

                return CommonResponse<DtoRegister>.Response(ExceptionMessageResponse, false, System.Net.HttpStatusCode.InternalServerError, register);
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
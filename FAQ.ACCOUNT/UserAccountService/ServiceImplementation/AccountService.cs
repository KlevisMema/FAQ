#region Usings
using FAQ.DAL.Models;
using FAQ.DAL.DataBase;
using FAQ.SHARED.ResponseTypes;
using FAQ.LOGGER.ServiceInterface;
using Microsoft.AspNetCore.Identity;
using FAQ.ACCOUNT.AccountService.ServiceInterface;
using FAQ.SHARED.ServicesMessageResponse;
using Microsoft.Extensions.Options;
#endregion

namespace FAQ.ACCOUNT.AccountService.ServiceImplementation
{
    /// <summary>
    ///     A Service class providing account functionalities by implementing IAccountService interface.
    /// </summary>
    public class AccountService : IAccountService
    {
        #region Services Injection

        /// <summary>
        ///     Log service
        /// </summary>
        private readonly ILogService _log;
        /// <summary>
        ///     User Manager service
        /// </summary>
        private readonly UserManager<User> _userManager;
        /// <summary>
        ///     Database Context
        /// </summary>
        private readonly ApplicationDbContext _db;
        /// <summary>
        ///     Message response register 
        /// </summary>
        private AccountMessageResponse _accountMessageResponse { get; set; } = new();
        /// <summary>
        ///     Exception message response
        /// </summary>
        private string ExceptionMessageResponse { get; set; }

        /// <summary>
        ///     Services Injection
        /// </summary>
        /// <param name="log"> Log Service </param>
        /// <param name="userManager"> User Manager Service </param>
        /// <param name="db"> Database Context </param>
        /// <param name="messageResponses"> Message response </param>
        public AccountService
        (
            ILogService log,
            ApplicationDbContext db,
            UserManager<User> userManager,
            IOptions<ServiceMessageResponseContainer> messageResponses
        )
        {
            _db = db;
            _log = log;
            _userManager = userManager;
            _accountMessageResponse = messageResponses.Value.AccountMessageResponse!;
            ExceptionMessageResponse = messageResponses.Value.Exception;
        }

        #endregion

        #region Method implementation

        /// <summary>
        ///     Confirm email of a user, method implementation.
        /// </summary>
        /// <param name="userId"> Id of the user of type <see cref="string"/> </param>
        /// <param name="otp"> otp of type <see cref="string"/>  </param>
        /// <returns> A Object response of type : <see cref="CommonResponse{T}"/> where T is <seealso cref="string"/> </returns>
        public async Task<CommonResponse<string>> ConfirmEmail
        (
            string userId,
            string otp
        )
        {
            try
            {
                if (String.IsNullOrEmpty(otp))
                    return CommonResponse<string>.Response(_accountMessageResponse.OtpEmpty, false, System.Net.HttpStatusCode.NotFound, otp);

                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                    return CommonResponse<string>.Response(_accountMessageResponse.UserNotFound, false, System.Net.HttpStatusCode.NotFound, string.Empty);

                if (!user.OTP.Equals(otp))
                    return CommonResponse<string>.Response(_accountMessageResponse.OtpCodeIncorrect, false, System.Net.HttpStatusCode.NotFound, otp);

                user.EmailConfirmed = true;

                _db.Users.Update(user);
                await _db.SaveChangesAsync();

                return CommonResponse<string>.Response(_accountMessageResponse.AccountConfirmed, true, System.Net.HttpStatusCode.OK, otp);

            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "Log In", Guid.Parse(userId));

                return CommonResponse<string>.Response(ExceptionMessageResponse, false, System.Net.HttpStatusCode.InternalServerError, string.Empty);
            }
        }

        #endregion
    }
}
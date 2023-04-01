#region Usings
using FAQ.SHARED.ResponseTypes;
using Microsoft.AspNetCore.Mvc;
using FAQ.ACCOUNT.AccountService.ServiceInterface;
#endregion

namespace FAQ.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {

        #region Services Injection

        private readonly IAccountService _accountService;

        /// <summary>
        ///     Inject services in constructor
        /// </summary>
        /// <param name="accountService"></param>
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        #endregion

        #region Endpoints

        /// <summary>
        ///     Confirm email of a user endpoint
        /// </summary>
        /// <param name="userId"> Id of the user </param>
        /// <returns> A object response </returns>

        [HttpPost("ConfirmEmail")]
        public async Task<CommonResponse<string>> ConfrimEmail(Guid userId)
        {
            var confirmEmailResult = await _accountService.ConfirmEmail(userId.ToString());

            return confirmEmailResult;
        }

        #endregion

    }
}

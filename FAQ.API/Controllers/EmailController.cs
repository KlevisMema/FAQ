#region Usings
using FAQ.DTO.UserDtos;
using FAQ.SHARED.ResponseTypes;
using Microsoft.AspNetCore.Mvc;
using FAQ.EMAIL.EmailService.ServiceInterface;
#endregion

namespace FAQ.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        #region Services Injection
        /// <summary>
        /// 
        /// </summary>
        private readonly IEmailSender _emailSender;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailSender"></param>
        public EmailController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        #endregion

        #region End Points
        /// <summary>
        ///     Send an confirmation email to  activate the account of the user
        /// </summary>
        /// <param name="confirmEmail"> Dto Object </param>
        /// <returns> A Response object </returns>
        [HttpPost]
        public async Task<ActionResult<CommonResponse<string>>> SendConfirmationEmail(DtoUserConfirmEmail confirmEmail)
        {
            var sendConfirmationEmailResult = await _emailSender.SendConfirmEmail(confirmEmail);

            return sendConfirmationEmailResult;
        } 
        #endregion
    }
}

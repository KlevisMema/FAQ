#region Usings
using FAQ.DTO.UserDtos;
using FAQ.SHARED.ResponseTypes;
using Microsoft.AspNetCore.Mvc;
using FAQ.EMAIL.EmailService.ServiceInterface;
using Microsoft.AspNetCore.Authorization;
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
       
        #endregion
    }
}

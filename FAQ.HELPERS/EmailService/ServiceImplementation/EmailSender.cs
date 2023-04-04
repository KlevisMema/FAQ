#region Usings
using System.Net;
using System.Net.Mail;
using FAQ.DTO.UserDtos;
using FAQ.EMAIL.EmailService;
using FAQ.SHARED.ResponseTypes;
using FAQ.LOGGER.ServiceInterface;
using Microsoft.Extensions.Options;
using FAQ.EMAIL.EmailService.ServiceInterface;
#endregion

namespace FAQ.HELPERS.Helpers.Email
{
    /// <summary>
    ///     A class that provides email sending, implements <see cref="IEmailSender"/> interface.
    /// </summary>
    public class EmailSender : IEmailSender
    {
        #region Services Injection

        /// <summary>
        ///     Email settings
        /// </summary>
        private readonly IOptions<EmailSettings> _emailSettigs;
        /// <summary>
        ///     Log service
        /// </summary>
        private readonly ILogService _log;

        /// <summary>
        ///     Inject services in  ctor
        /// </summary>
        /// <param name="emailSettigs"> Email settigs/options </param>
        /// <param name="log"> Logger Service </param>
        public EmailSender
        (
            ILogService log,
            IOptions<EmailSettings> emailSettigs
        )
        {
            _log = log;
            _emailSettigs = emailSettigs;
        }

        #endregion

        #region Methods Implementation

        /// <summary>
        ///     Send email form email confirmation, method  implementation.
        /// </summary>
        /// <param name="userConfirmEmail"> User object Dto of type <see cref="DtoUserConfirmEmail"/> </param>
        /// <returns> <see cref="CommonResponse{T}"/> where T is <see cref="string"/> </returns>
        public async Task<CommonResponse<string>> SendConfirmEmail
        (
            DtoUserConfirmEmail userConfirmEmail,
            string otp
        )
        {
            try
            {
                MailMessage message = new()
                {
                    From = new MailAddress(_emailSettigs.Value.From),
                    Subject = "Email cofirmation",
                    IsBodyHtml = _emailSettigs.Value.IsBodyHtml,
                    Body = $"Your one time password is : {otp}"
                };

                message.To.Add(new MailAddress(userConfirmEmail.Email));

                SmtpClient smtpClient = new()
                {
                    Port = _emailSettigs.Value.Port,
                    Host = "smtp.gmail.com",
                    EnableSsl = _emailSettigs.Value.UseSSL,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(_emailSettigs.Value.SmtpUsername, _emailSettigs.Value.SmtpPassword),
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };

                await smtpClient.SendMailAsync(message);

                await _log.CreateLogAction($"User with email : {userConfirmEmail.Email} was sent an email with the otp : {otp}", "SendConfirmEmail", userConfirmEmail.UserId);

                return CommonResponse<string>.Response($"An confirmation email was sent to the user : {userConfirmEmail.Email}", true, HttpStatusCode.OK, string.Empty);

            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "SendConfirmEmail", userConfirmEmail.UserId);

                return CommonResponse<string>.Response($"Internal server error", false, HttpStatusCode.InternalServerError, string.Empty);
            }
        }

        #endregion
    }
}
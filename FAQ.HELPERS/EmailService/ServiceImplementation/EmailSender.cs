#region Usings
using System.Net;
using System.Net.Mail;
using FAQ.DTO.UserDtos;
using FAQ.EMAIL.EmailService;
using Microsoft.Extensions.Options;
using FAQ.EMAIL.EmailService.ServiceInterface;
using FAQ.SHARED.ResponseTypes;
using static System.Net.WebRequestMethods;
#endregion

namespace FAQ.HELPERS.Helpers.Email
{
    /// <summary>
    ///     A class that provides email sending, implements IEmailSender interface.
    /// </summary>
    public class EmailSender : IEmailSender
    {
        #region Services Injection
        /// <summary>
        ///     Email settings
        /// </summary>
        private readonly IOptions<EmailSettings> _emailSettigs;

        /// <summary>
        ///     Inject services in  ctor
        /// </summary>
        /// <param name="emailSettigs"> Email settigs/options </param>
        public EmailSender(IOptions<EmailSettings> emailSettigs)
        {
            _emailSettigs = emailSettigs;
        }
        #endregion

        #region Implementation

        /// <summary>
        ///     Send email form email confirmation
        /// </summary>
        /// <param name="userConfirmEmail"> User object Dto </param>
        /// <returns> nothing </returns>
        public async Task<CommonResponse<string>> SendConfirmEmail(DtoUserConfirmEmail userConfirmEmail)
        {
            try
            {
                MailMessage message = new()
                {
                    From = new MailAddress(_emailSettigs.Value.From),
                    Subject = "Email cofirmation",
                    IsBodyHtml = _emailSettigs.Value.IsBodyHtml,
                    Body = $"Click this link to confirm your account https://localhost:7092/api/Account/ConfirmEmail/{userConfirmEmail.UserId}"
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

                return CommonResponse<string>.Response($"An confirmation email was sent to the user : {userConfirmEmail.Email}", true, HttpStatusCode.OK, string.Empty);

            }
            catch (Exception)
            {
                return CommonResponse<string>.Response($"Internal server error", false, HttpStatusCode.InternalServerError, string.Empty);
            }
        }

        #endregion
    }
}
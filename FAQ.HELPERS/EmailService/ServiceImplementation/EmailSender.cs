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
        /// <returns> Nothing </returns>
        public async Task 
        SendConfirmEmail
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
                    Subject = _emailSettigs.Value.Subject,
                    IsBodyHtml = _emailSettigs.Value.IsBodyHtml,
                    Body = $"""
                                <div style="font-family: Helvetica,Arial,sans-serif;min-width:1000px;overflow:auto;line-height:2">
                                  <div style="margin:50px auto;width:70%;padding:20px 0">
                                    <div style="border-bottom:1px solid #eee">
                                      <a href="" style="font-size:1.4em;color: #00466a;text-decoration:none;font-weight:600">FAQ-Q</a>
                                    </div>
                                    <p style="font-size:1.1em">Hi,</p>
                                    <p>{_emailSettigs.Value.Body.Replace("{OTP}", otp)}</p>
                                    <h2 style="background: #00466a;margin: 0 auto;width: max-content;padding: 0 10px;color: #fff;border-radius: 4px;">324457</h2>
                                    <p style="font-size:0.9em;">Regards,<br />FAQ-Q</p>
                                    <hr style="border:none;border-top:1px solid #eee" />
                                    <div style="float:right;padding:8px 0;color:#aaa;font-size:0.8em;line-height:1;font-weight:300">
                                      <p>FAQ-Q</p>
                                      <p>Albania</p>
                                    </div>
                                  </div>
                                </div>
                            """
                };

                message.To.Add(new MailAddress(userConfirmEmail.Email));

                SmtpClient smtpClient = new()
                {
                    Port = _emailSettigs.Value.Port,
                    Host = _emailSettigs.Value.Host,
                    EnableSsl = _emailSettigs.Value.UseSSL,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(_emailSettigs.Value.SmtpUsername, _emailSettigs.Value.SmtpPassword),
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };

                await smtpClient.SendMailAsync(message);

                await _log.CreateLogAction($"User with email : {userConfirmEmail.Email} was sent an email with the otp : {otp}", "SendConfirmEmail", userConfirmEmail.UserId);

            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "SendConfirmEmail", userConfirmEmail.UserId);
            }
        }

        /// <summary>
        ///     Notify dev team if something went wrong.
        /// </summary>
        /// <param name="userId"> Id of the user </param>
        /// <returns> Nothig </returns>
        public async Task 
        SendEmailToDevTeam
        (
           Guid userId
        )
        {
            try
            {
                MailMessage message = new()
                {
                    From = new MailAddress(_emailSettigs.Value.From),
                    Subject = _emailSettigs.Value.Subject,
                    IsBodyHtml = _emailSettigs.Value.IsBodyHtml,
                    Body = $"""
                                A problem happened to the application for user {userId}
                                go and check db. Time when happened {DateTime.Now}.
                            """
                };

                message.To.Add(new MailAddress(_emailSettigs.Value.From));

                SmtpClient smtpClient = new()
                {
                    Port = _emailSettigs.Value.Port,
                    Host = _emailSettigs.Value.Host,
                    EnableSsl = _emailSettigs.Value.UseSSL,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(_emailSettigs.Value.SmtpUsername, _emailSettigs.Value.SmtpPassword),
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };

                await smtpClient.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "SendEmailToDevTeam", userId);
            }
        }

        #endregion
    }
}
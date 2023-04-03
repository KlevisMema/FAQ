using FAQ.DTO.UserDtos;
using FAQ.SHARED.ResponseTypes;

namespace FAQ.EMAIL.EmailService.ServiceInterface
{
    /// <summary>
    ///     A interface that provides email sending.
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        ///     Send email form email confirmation
        /// </summary>
        /// <param name="userConfirmEmail"> User object Dto </param>
        /// <param name="otp"> One time password </param>
        /// <returns> a object result </returns>
        Task<CommonResponse<string>> SendConfirmEmail(DtoUserConfirmEmail userConfirmEmail, string otp);
    }
}
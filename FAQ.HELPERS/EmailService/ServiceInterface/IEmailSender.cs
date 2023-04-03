using FAQ.DTO.UserDtos;
using FAQ.SHARED.ResponseTypes;

namespace FAQ.EMAIL.EmailService.ServiceInterface
{
    /// <summary>
    ///     A interface that provides email sending.
    /// </summary>
    public interface IEmailSender
    {
        Task<CommonResponse<string>> SendConfirmEmail(DtoUserConfirmEmail userConfirmEmail);
    }
}
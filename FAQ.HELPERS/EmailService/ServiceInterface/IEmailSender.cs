#region Usings
using FAQ.DTO.UserDtos;
using FAQ.SHARED.ResponseTypes;
#endregion

namespace FAQ.EMAIL.EmailService.ServiceInterface
{
    /// <summary>
    ///     A interface that provides email sending.
    /// </summary>
    public interface IEmailSender
    {
        #region Methods Declaration

        /// <summary>
        ///     Send email form email confirmation, method  declaration.
        /// </summary>
        /// <param name="userConfirmEmail"> User object Dto of type <see cref="DtoUserConfirmEmail"/> </param>
        /// <param name="otp"> One time password value of type <see cref="string"/> </param>
        /// <returns> <see cref="CommonResponse{T}"/> where T is <see cref="string"/> </returns>
        Task SendConfirmEmail
        (
            DtoUserConfirmEmail userConfirmEmail,
            string otp
        );
        /// <summary>
        ///     Notify dev team if something went wrong.
        /// </summary>
        /// <param name="userId"> Id of the user </param>
        /// <returns> Nothig </returns>
        Task SendEmailToDevTeam
       (
          Guid userId
       );

        #endregion
    }
}
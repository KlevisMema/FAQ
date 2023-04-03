#region Usings
using FAQ.SHARED.ResponseTypes;
#endregion

namespace FAQ.ACCOUNT.AccountService.ServiceInterface
{
    /// <summary>
    ///     A interface providing account functions declarations.
    /// </summary>
    public interface IAccountService
    {
        #region Methods declarations
        /// <summary>
        ///     Confirm email of a user, method declaration.
        /// </summary>
        /// <param name="userId"> Id of the user of type <see cref="string"/> </param>
        /// <param name="otp"> otp of type <see cref="string"/>  </param>
        /// <returns> A Object response of type : <see cref="CommonResponse{T}"/> where T is <seealso cref="string"/> </returns>
        Task<CommonResponse<string>> ConfirmEmail(string userId, string otp);
        #endregion
    }
}
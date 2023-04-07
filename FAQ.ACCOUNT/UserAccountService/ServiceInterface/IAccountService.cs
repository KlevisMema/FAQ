#region Usings
using FAQ.DTO.UserDtos;
using FAQ.SHARED.ResponseTypes;
using Microsoft.AspNetCore.Hosting;
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
        Task<CommonResponse<string>> ConfirmEmail
        (
            string userId,
            string otp
        );
        /// <summary>
        ///     Upload profile picture for the user, method declaration.
        /// </summary>
        /// <param name="userId"> <see cref="Guid"/> ID of the user </param>
        /// <param name="picUpload"> <see cref="DtoProfilePicUpload"/> object </param>
        /// <param name="webHostEnvironment"> <see cref="IWebHostEnvironment"/> information </param>
        /// <returns> <see cref="CommonResponse{T}"/> where T is <see cref="DtoProfilePicUpload"/> </returns>
        Task<CommonResponse<DtoProfilePicUpload>> UploadProfilePicture
        (
            Guid userId,
            DtoProfilePicUpload picUpload,
            IWebHostEnvironment webHostEnvironment
        );
        #endregion
    }
}
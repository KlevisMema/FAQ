#region Usings
using FAQ.DTO.UserDtos;
using FAQ.SHARED.ResponseTypes;
#endregion

namespace FAQ.ACCOUNT.AuthorizationService.Interfaces
{
    /// <summary>
    ///     A interface that provides the register method.
    /// </summary>
    public interface IRegisterService
    {
        #region Method Declaration
        /// <summary>
        ///     Register a user method declaration
        /// </summary>
        /// <param name="register"> Dto register object </param>
        /// <returns> A  object  response of <see cref="CommonResponse{T}"/>' where <see langword="T"/> is <seealso cref="DtoRegister"/> </returns>
        Task<CommonResponse<DtoRegister>> 
        Register
        (
            DtoRegister register
        );
        #endregion
    }
}
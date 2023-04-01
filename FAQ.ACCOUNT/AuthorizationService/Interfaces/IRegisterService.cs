#region Usings
using FAQ.DTO.UserDtos;
using FAQ.SHARED.ResponseTypes; 
#endregion

namespace FAQ.ACCOUNT.AuthorizationService.Interfaces
{
    /// <summary>
    ///     A interface  that provides the register method declaration.
    /// </summary>
    public interface IRegisterService
    {
        /// <summary>
        ///     Register a user declaration
        /// </summary>
        /// <param name="register"> Dto </param>
        /// <returns> A  object  response of type 'CommonResponse' </returns>
        Task<CommonResponse<DtoRegister>> Register(DtoRegister register);
    }
}
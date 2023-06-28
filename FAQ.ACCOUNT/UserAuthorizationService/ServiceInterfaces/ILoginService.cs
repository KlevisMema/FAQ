#region Usings
using FAQ.DTO.UserDtos;
using FAQ.SHARED.ResponseTypes;
#endregion

namespace FAQ.ACCOUNT.AuthorizationService.Interfaces
{
    /// <summary>
    ///     A interface that provides the login method.
    /// </summary>
    public interface ILoginService
    {
        #region Method Declaration
        /// <summary>
        ///     Log in a user and genereate a token method declaration.
        /// </summary>
        /// <param name="logIn"> Login object of type <see cref="DtoLogin"/> </param>
        /// <returns> A object response of <see cref="CommonResponse{DtoLogin}"/> where <see langword="T"/> is <seealso cref="DtoLogin"/> </returns>
        Task<CommonResponse<DtoLogin>> 
        Login
        (
            DtoLogin logIn
        );
        #endregion
    }
}
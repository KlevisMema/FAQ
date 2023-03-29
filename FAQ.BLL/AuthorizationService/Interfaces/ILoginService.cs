#region Usings
using FAQ.DTO.UserDtos;
using FAQ.SHARED.ResponseTypes; 
#endregion

namespace FAQ.SERVICES.AuthorizationService.Interfaces
{
    public interface ILoginService
    {
        /// <summary>
        ///     Log in a user and genereate a token
        /// </summary>
        /// <param name="logIn"> Login object </param>
        Task<CommonResponse<LoginViewModel>> Login(LoginViewModel logIn);
    }
}
#region Usings
using FAQ.DTO.UserDtos;
#endregion

namespace FAQ.ACCOUNT.AuthenticationService.ServiceInterface
{
    /// <summary>
    ///     A interface that provides a token generation.
    /// </summary>
    public interface IOAuthJwtTokenService
    {
        #region Method declarations
        /// <summary>
        ///     Serialize a token in a string fromat (Jwt format), method declaration.
        /// </summary>
        /// <param name="user"> User View Model object of type <see cref="DtoUser"/> </param>
        /// <returns> Token of type <see cref="string"/></returns>
        string CreateToken
        (
            DtoUser user
        );
        #endregion
    }
}
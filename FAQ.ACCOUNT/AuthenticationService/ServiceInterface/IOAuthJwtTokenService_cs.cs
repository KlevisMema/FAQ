using FAQ.DTO.UserDtos;

namespace FAQ.ACCOUNT.AuthenticationService.ServiceInterface
{
    public interface IOAuthJwtTokenService
    {
        /// <summary>
        ///     Serialize a token in a string fromat (Jwt format)
        /// </summary>
        /// <param name="user"> User View Model object</param>
        /// <returns>Token</returns>
        string CreateToken(DtoUser user);
    }
}
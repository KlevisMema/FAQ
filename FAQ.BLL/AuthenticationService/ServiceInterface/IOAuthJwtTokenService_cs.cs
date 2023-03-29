using FAQ.DTO.UserDtos;

namespace FAQ.SERVICES.AuthenticationService.ServiceInterface
{
    public interface IOAuthJwtTokenService
    {
        /// <summary>
        ///     Serialize a token in a string fromat (Jwt format)
        /// </summary>
        /// <param name="user"> User View Model object</param>
        /// <returns>Token</returns>
        string CreateToken(UserViewModel user);
    }
}
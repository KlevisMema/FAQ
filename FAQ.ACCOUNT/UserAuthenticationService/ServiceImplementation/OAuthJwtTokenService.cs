#region Usings
using System.Text;
using FAQ.DTO.UserDtos;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using FAQ.ACCOUNT.AuthenticationService.ServiceInterface;
#endregion

namespace FAQ.ACCOUNT.AuthenticationService.ServiceImplementation
{
    /// <summary>
    ///     A class that provides a token generation, implements the <see cref="IOAuthJwtTokenService"/> interface.
    /// </summary>
    public class OAuthJwtTokenService : IOAuthJwtTokenService
    {
        #region Properties and Constructor
        /// <summary>
        ///     Jwt options/settings of type <see cref="IOptions{TOptions}"/> where T is <see cref="AuthenticationSettings"/>.
        /// </summary>
        private readonly IOptions<AuthenticationSettings> _jwtOptions;

        /// <summary>
        ///     Inject options/settigs in the constructor.
        /// </summary>
        /// <param name="jwtOptions"> Jwt oprtions/settings </param>
        public OAuthJwtTokenService
        (
            IOptions<AuthenticationSettings> jwtOptions
        )
        {
            _jwtOptions = jwtOptions;
        }
        #endregion

        #region Methods implementations.
        /// <summary>
        ///     Serialize a token in a string fromat (Jwt format), method implementation.
        /// </summary>
        /// <param name="user"> User View Model object of type <see cref="DtoUser"/> </param>
        /// <returns> Token of type <see cref="string"/></returns>
        public string 
        CreateToken
        (
            DtoUser user
        )
        {
            var singinCredentials = GetSinginCredentials();
            var claims = GetClaims(user);
            var token = GenerateToken(singinCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        /// <summary>
        ///     Get jwt config key and hash it.
        /// </summary>
        /// <returns> <see cref="SigningCredentials"/> hashed key </returns>
        private SigningCredentials 
        GetSinginCredentials()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.Key));

            return new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        }
        /// <summary>
        ///     Creates a list of claims.
        /// </summary>
        /// <param name="user"> User View Model object of type <see cref="DtoUser"/> </param>
        /// <returns>List of claims <see cref="List{Claim}"/> where T is <seealso cref="Claim"/></returns>
        private List<Claim> 
        GetClaims
        (
            DtoUser user
        )
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(JwtRegisteredClaimNames.NameId, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iss, _jwtOptions.Value.Issuer)
            };

            claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role)));

            return claims;
        }
        /// <summary>
        ///     Create a token with hashed credentials and a list of claims.
        /// </summary>
        /// <param name="singinCredentials"> Hashed credentials of type <see cref="SigningCredentials"/> </param>
        /// <param name="claims"> List of claims of type <see cref="Claim"/> </param>
        /// <returns> JwtSecurityToken of type <see cref="JwtSecurityToken"/> </returns>
        private JwtSecurityToken 
        GenerateToken
        (
            SigningCredentials singinCredentials,
            List<Claim> claims
        )
        {
            var token = new JwtSecurityToken
            (
                issuer: _jwtOptions.Value.Issuer,
                claims: claims,
                expires: DateTime.Now.AddHours(Convert.ToDouble(_jwtOptions.Value.LifeTime)),
                signingCredentials: singinCredentials
            );

            return token;
        }

        #endregion
    }
}
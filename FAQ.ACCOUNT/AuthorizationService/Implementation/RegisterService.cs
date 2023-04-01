#region Usings
using AutoMapper;
using FAQ.DAL.Models;
using FAQ.DTO.UserDtos;
using FAQ.SHARED.ResponseTypes;
using Microsoft.AspNetCore.Identity;
using FAQ.SERVICES.RepositoryService.Interfaces;
using FAQ.ACCOUNT.AuthorizationService.Interfaces;
using FAQ.DAL.DataBase;
using Microsoft.EntityFrameworkCore;
#endregion

namespace FAQ.ACCOUNT.AuthorizationService.Implementation
{
    /// <summary>
    ///     A service class providing the registration.
    /// </summary>
    public class RegisterService : IRegisterService
    {
        #region Services Injection

        /// <summary>
        ///    A readonly field for Mapper service
        /// </summary>
        private readonly IMapper _mapper;
        /// <summary>
        ///   A readonly field for  User Manager
        /// </summary>
        private readonly UserManager<User> _userManager;
        /// <summary>
        ///     Database Context
        /// </summary>
        private readonly ApplicationDbContext _db;

        /// <summary>
        ///     Log service
        /// </summary>
        private readonly ILogService _log;

        /// <summary>
        ///     Inject all services in constructor
        /// </summary>
        /// <param name="userManager"> User Manager service </param>
        /// <param name="mapper"> Auto mapper </param>
        /// <param name="log"> Log service </param>
        /// <param name="db"> Database context </param>
        public RegisterService
        (
            IMapper mapper,
            ILogService log,
            UserManager<User> userManager,
            ApplicationDbContext db
        )
        {
            _log = log;
            _mapper = mapper;
            _userManager = userManager;
            _db = db;
        }

        #endregion

        #region Method

        /// <summary>
        ///     Register a user  implementation
        /// </summary>
        /// <param name="register"> Dto register object </param>
        /// <returns> A  object  response of type 'CommonResponse' </returns>
        public async Task<CommonResponse<DtoRegister>> Register
        (
           DtoRegister register
        )
        {
            try
            {
                var user = _mapper.Map<User>(register);

                user.IsAdmin = false;
                user.Created = DateTime.Now;
                user.TwoFactorEnabled = false;
                user.EmailConfirmed = false;

                var result = await _userManager.CreateAsync(user, register.Password);

                if (result.Succeeded)
                {
                    var registeredUser = await _userManager.FindByEmailAsync(register.Email);

                    var role = await _db.Roles.FirstOrDefaultAsync(x => x.Name!.Equals("User"));

                    if (role is not null)
                        await _userManager.AddToRoleAsync(user, role.Name!);

                    if (registeredUser is not null)
                        await _log.CreateLogAction($"[Succsess] - A user just registered at {DateTime.Now} with the email {registeredUser.Email}", "Register", Guid.Parse(registeredUser.Id));

                    return CommonResponse<DtoRegister>.Response($"Register succsessful", true, System.Net.HttpStatusCode.OK, register);
                }

                return CommonResponse<DtoRegister>.Response("User registration attempt failed", false, System.Net.HttpStatusCode.BadRequest, new DtoRegister());
            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "Register", null);

                return CommonResponse<DtoRegister>.Response("Internal server error", false, System.Net.HttpStatusCode.InternalServerError, new DtoRegister());
            }
        }

        #endregion
    }
}
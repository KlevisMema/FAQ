using AutoMapper;
using Azure;
using FAQ.DAL.DataBase;
using FAQ.DAL.Models;
using FAQ.DTO.UserDtos;
using FAQ.SERVICES.AuthorizationService.Interfaces;
using FAQ.SHARED.ResponseTypes;
using Microsoft.AspNetCore.Identity;

namespace FAQ.SERVICES.AuthorizationService.Implementation
{
    public class RegisterService : IRegisterService
    {
        /// <summary>
        ///    A readonly field for Mapper service
        /// </summary>
        private readonly IMapper _mapper;
        /// <summary>
        ///   A readonly field for  User Manager
        /// </summary>
        private readonly UserManager<User> _userManager;
        /// <summary>
        ///     A readonly field for role manager
        /// </summary>
        private readonly RoleManager<IdentityRole> _roleManager;
        /// <summary>
        ///     Database context
        /// </summary>
        private readonly ApplicationDbContext _db;

        /// <summary>
        ///     Inject all services in constructor
        /// </summary>
        /// <param name="userManager"> User Manager service </param>
        /// <param name="db"> Database context </param>
        /// <param name="roleManager"> Role manager </param>
        public RegisterService
        (
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db,
            IMapper mapper
        )
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<CommonResponse<RegisterViewModel>> Register
        (
           RegisterViewModel register
        )
        {
            try
            {
                var user = _mapper.Map<User>(register);

                var result = await _userManager.CreateAsync(user, register.Password);

                if (result.Succeeded)
                    return CommonResponse<RegisterViewModel>.Response($"Register succsessful, {result.Errors}", true, System.Net.HttpStatusCode.OK, register);

                return CommonResponse<RegisterViewModel>.Response("User registration attempt failed", false, System.Net.HttpStatusCode.BadRequest, new RegisterViewModel());
            }
            catch (Exception ex)
            {
                return CommonResponse<RegisterViewModel>.Response("Iternal server error", false, System.Net.HttpStatusCode.InternalServerError, new RegisterViewModel());
            }
        }
    }
}

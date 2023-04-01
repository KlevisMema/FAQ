#region Usings
using FAQ.DAL.Models;
using FAQ.SHARED.ResponseTypes;
using Microsoft.AspNetCore.Identity;
using FAQ.SERVICES.RepositoryService.Interfaces;
using FAQ.ACCOUNT.AccountService.ServiceInterface;
using FAQ.DAL.DataBase;
#endregion

namespace FAQ.ACCOUNT.AccountService.ServiceImplementation
{
    public class AccountService : IAccountService
    {
        #region Services Injection

        /// <summary>
        ///     Log service
        /// </summary>
        private readonly ILogService _log;
        /// <summary>
        ///     User Manager service
        /// </summary>
        private readonly UserManager<User> _userManager;
        /// <summary>
        ///     Database Context
        /// </summary>
        private readonly ApplicationDbContext _db;
        /// <summary>
        ///     Role Manager Service
        /// </summary>
        private readonly RoleManager<IdentityRole> _roleManager;

        /// <summary>
        ///     Services Injection
        /// </summary>
        /// <param name="log"> Log Service </param>
        /// <param name="userManager"> User Manager Service </param>
        /// <param name="db"> Database Context </param>
        /// <param name="roleManager"> Role Manager </param>
        public AccountService
        (
            ILogService log,
            ApplicationDbContext db,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager
        )
        {
            _db = db;
            _log = log;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Create a new role
        /// </summary>
        /// <param name="roleName"> Name of the role </param>
        /// <param name="userId"> Id of the user </param>
        /// <returns> A object resposne </returns>
        public async Task<CommonResponse<string>> CreateRole(string roleName, string userId)
        {
            try
            {
                if (String.IsNullOrEmpty(roleName))
                    return CommonResponse<string>.Response("Role name is empty", false, System.Net.HttpStatusCode.BadRequest, roleName);

                var result = await _roleManager.CreateAsync(new IdentityRole { Name = roleName });

                if (result.Succeeded)
                    return CommonResponse<string>.Response("Role created succsessfully", true, System.Net.HttpStatusCode.OK, roleName);

                return CommonResponse<string>.Response("Role created unsuccsessfully", false, System.Net.HttpStatusCode.BadRequest, roleName);

            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "Log In", Guid.Parse(userId));

                return CommonResponse<string>.Response("Internal Server error.", false, System.Net.HttpStatusCode.InternalServerError, roleName);
            }
        }

        /// <summary>
        ///     Confirm email of a user
        /// </summary>
        /// <param name="userId"> Id of the user </param>
        /// <returns> A Object response </returns>
        public async Task<CommonResponse<string>> ConfirmEmail(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                    return CommonResponse<string>.Response("User not found", false, System.Net.HttpStatusCode.NotFound, string.Empty);

                user.EmailConfirmed = true;

                _db.Users.Update(user);
                await _db.SaveChangesAsync();

                return CommonResponse<string>.Response("User account confirmed succsessfully", true, System.Net.HttpStatusCode.OK, string.Empty);

            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "Log In", Guid.Parse(userId));

                return CommonResponse<string>.Response("Internal Server error.", false, System.Net.HttpStatusCode.InternalServerError, string.Empty);
            }
        }

        #endregion
    }
}

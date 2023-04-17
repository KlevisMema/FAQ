#region Usings
using FAQ.DAL.DataBase;
using FAQ.DAL.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
#endregion

namespace FAQ.DAL.Seeders
{
    /// <summary>
    ///     A Seeder Class for users.
    /// </summary>
    public class AccountsSeeder
    {
        #region Method implementation

        /// <summary>
        ///     Seed Users with roles, users are retrieved from appsettings.json.
        /// </summary>
        /// <param name="applicationBuilder"> App Builder of type <see cref="IApplicationBuilder"/> </param>
        /// <param name="configuration"> Configuration of type <see cref="IConfiguration"/> </param>
        /// <returns> Nothing </returns>
        public static async Task SeedUsersAsync
        (
            IApplicationBuilder applicationBuilder, 
            IConfiguration configuration
        )
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();

            var _context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

            if (_context is not null)
            {
                _context.Database.EnsureCreated();

                var getUsers = configuration.GetSection(AccountSettings.SectionName).Get<AccountSettings[]>();

                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();

                foreach (var item in getUsers!)
                {
                    var User = await userManager.FindByEmailAsync(item.UserName);

                    if (User == null)
                    {
                        var newUser = new User()
                        {
                            UserName = item.UserName,
                            Email = item.UserName,
                            EmailConfirmed = true,
                            Gender = SHARED.Enums.Gender.Male,
                            Name = item.UserName,
                            Surname = item.SurnName,
                            Adress = item.Adress,
                            Age = item.Age
                        };

                        await userManager.CreateAsync(newUser, item.Password);

                        foreach (var role in item.Roles)
                        {
                            await userManager.AddToRoleAsync(newUser, role);
                        }
                    }
                }
            }
        }

        #endregion
    }
}

#region Usings
using FAQ.DAL.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection; 
#endregion

namespace FAQ.DAL.Seeders
{
    /// <summary>
    ///     A Seeder Class for users 
    /// </summary>
    public class AccountsSeeder
    {
        /// <summary>
        ///     Seed Users with roles
        /// </summary>
        /// <param name="applicationBuilder"> App Builder </param>
        /// <param name="configuration"> Configuration </param>
        /// <returns> Nothing </returns>
        public static async Task SeedUsersAsync(IApplicationBuilder applicationBuilder, IConfiguration configuration)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();

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
}

#region Usings
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
#endregion

namespace FAQ.DAL.Seeders
{
    /// <summary>
    ///     A Seeder class for roles.
    /// </summary>
    public class RolesSeeder
    {
        #region Method implementation

        /// <summary>
        ///     Create roles, roles are recieved from appsettings.json.
        /// </summary>
        /// <param name="applicationBuilder"> App Builder of type <see cref="IApplicationBuilder"/> </param>
        /// <param name="configuration"> Cofiguration of type <see cref="IConfiguration"/> </param>
        /// <returns></returns>
        public static async Task SeedRolesAsync(IApplicationBuilder applicationBuilder, IConfiguration configuration)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();

            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync(configuration.GetSection("Roles:Admin").Value!))
                await roleManager.CreateAsync(new IdentityRole(configuration.GetSection("Roles:Admin").Value!));
            if (!await roleManager.RoleExistsAsync(configuration.GetSection("Roles:User").Value!))
                await roleManager.CreateAsync(new IdentityRole(configuration.GetSection("Roles:User").Value!));
            if (!await roleManager.RoleExistsAsync(configuration.GetSection("Roles:Employee").Value!))
                await roleManager.CreateAsync(new IdentityRole(configuration.GetSection("Roles:Employee").Value!));
        }

        #endregion
    }
}

#region Usings
using FAQ.DAL.Seeders;
#endregion

namespace FAQ.API.Startup
{
    /// <summary>
    ///     A static class that provides a extension of services registration after app is build.
    /// </summary>
    public static class AfterAppBuildExtesion
    {
        #region Methods

        /// <summary>
        ///     Call  seed roles and seed users methods
        /// </summary>
        /// <param name="app"> Web app </param>
        /// <param name="Configuration"> A collection of configurations </param>
        /// <returns> Nothing </returns>
        public static async Task CallSeedersAsync(this WebApplication app, IConfiguration Configuration)
        {
            // Seed roles 
            await RolesSeeder.SeedRolesAsync(app, Configuration);
            // seed users
            await AccountsSeeder.SeedUsersAsync(app, Configuration);
        } 

        #endregion
    }
}
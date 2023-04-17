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
    ///     A Seeder class for log types.
    /// </summary>
    public class LogTypesSeeder
    {
        #region Method implementation

        /// <summary>
        ///     Create log types.
        /// </summary>
        /// <param name="applicationBuilder"> App Builder of type <see cref="IApplicationBuilder"/> </param>
        /// <param name="configuration"> Cofiguration of type <see cref="IConfiguration"/> </param>
        /// <returns> Nothing </returns>
        public static async Task SeedLogTypesAsync
        (
            IApplicationBuilder applicationBuilder
        )
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();

            var _context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

            if (_context is not null)
            {
                await _context.LogTypes.AddRangeAsync(new List<LogType>()
                {
                    new LogType{Id=1, Name="Exception"},
                    new LogType{Id=2, Name="User Action"},
                });

                await _context.SaveChangesAsync();
            }

        }

        #endregion
    }
}

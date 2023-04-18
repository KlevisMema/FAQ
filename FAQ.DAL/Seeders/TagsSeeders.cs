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
    ///     A Seeder class for tags.
    /// </summary>
    public class TagsSeeders
    {
        #region Method implementation

        /// <summary>
        ///     Create tags.
        /// </summary>
        /// <param name="applicationBuilder"> App Builder of type <see cref="IApplicationBuilder"/> </param>
        /// <param name="configuration"> Cofiguration of type <see cref="IConfiguration"/> </param>
        /// <returns> Nothing </returns>
        public static async Task SeedTagsAsync
        (
            IApplicationBuilder applicationBuilder
        )
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();

            var _context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

            if (_context is not null)
            {
                if (!_context.Tags.Any())
                {
                    await _context.Tags.AddRangeAsync(new List<Tag>()
                    {
                        new Tag{Id=Guid.NewGuid(), CreatedAt= DateTime.Now, IsDeleted = false, Name="Cars"},
                    });

                    await _context.SaveChangesAsync();
                }
            }

            #endregion
        }
    }
}
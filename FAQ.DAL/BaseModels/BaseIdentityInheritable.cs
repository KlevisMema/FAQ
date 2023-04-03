#region Usings
using Microsoft.AspNetCore.Identity;
#endregion

namespace FAQ.DAL.BaseModels
{
    /// <summary>
    ///     A base model inheriting from Identity User.
    /// </summary>
    public abstract class BaseIdentityInheritable : IdentityUser
    {
        #region Properties

        /// <summary>
        ///     DateTime field prop, which will hold the value of the date and time 
        ///     when a record is created for this user, functionality provided in mapper service.
        /// </summary>
        public DateTime? Created { get; set; }
        /// <summary>
        ///     DateTime field prop, which will hold the value of the date and time 
        ///     when a record is edited for this user, functionality provided in mapper mapper.
        /// </summary>
        public DateTime? Edited { get; set; }
        /// <summary>
        ///     DateTime field prop, which will hold the value of the date and time 
        ///     when a record is deleted for this user, functionality provided in mapper service.
        /// </summary>
        public DateTime? Deleted { get; set; }
        /// <summary>
        ///     Bool field prop, which will hold the value if the user is admin or not
        ///     for this user, functionality provided in service layer.
        /// </summary>
        public bool IsAdmin { get; set; }

        #endregion
    }
}
#region Usings
using FAQ.DAL.Models;
using System.ComponentModel.DataAnnotations;
#endregion

namespace FAQ.DAL.BaseModels
{
    /// <summary>
    ///     A base model class which inherits from <see cref="BaseInheritable"/> model with all its props
    ///     and provides its own property. Its inherited by <see cref="Answer"/> and <see cref="Question"/>
    /// </summary>
    public abstract class BaseUserInheritable : BaseInheritable
    {
        #region Properites
        /// <summary>
        ///     A <see cref="Guid"/> prop, which will serve as a foreign key.
        /// </summary>
        [Required]
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        ///     A <see cref="User"/> prop, which has all the user properties inside it.
        /// </summary>
        public virtual User? User { get; set; }
        #endregion
    }
}
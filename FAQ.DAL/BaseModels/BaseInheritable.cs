#region Usings
using FAQ.DAL.Models;
#endregion

namespace FAQ.DAL.BaseModels
{
    /// <summary>
    ///     A base model class which has generic props 
    ///     to be inherited from models : <see cref="Tag"/> and <see cref="BaseUserInheritable"/>
    /// </summary>
    public abstract class BaseInheritable
    {
        #region Properties

        /// <summary>
        ///     Date time when this record was created, nullable.
        /// </summary>
        public DateTime? CreatedAt { get; set; }
        /// <summary>
        ///     Date time when this record was edited, nullable.
        /// </summary>
        public DateTime? EditedAt { get; set; }
        /// <summary>
        ///     Date time when this record was deleted, nullable.
        /// </summary>
        public DateTime? DeletedAt { get; set; }
        /// <summary>
        ///     <see langword="true"/> if record is deleted 
        ///     <see langword="false"/> if record is no deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        #endregion
    }
}
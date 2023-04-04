#region Usings
using System.ComponentModel.DataAnnotations;
#endregion

namespace FAQ.DAL.Models
{
    /// <summary>
    ///     A model class that represents the structure of the table LogType 
    /// </summary>
    public class LogType
    {
        #region Properties

        /// <summary>
        ///     A <see cref="int"/> property which has no default value.
        ///     This property is configured to be the primary key of Log table using <see cref="KeyAttribute"/>.
        ///     Will hold the id of log type table.
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        ///     A <see cref="string"/> property which has a empty string as default value, <see cref="string.Empty"/>.
        ///     Will hold the value of the name of log type.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        ///     A property used for inhertiance purposes with <see cref="Log"/> model and lazy loading.
        ///     Will hold the logs of this log type.
        ///     It's nullable.
        /// </summary>
        public virtual ICollection<Log>? Logs { get; set; }

        #endregion
    }
}
using System.ComponentModel.DataAnnotations;

namespace FAQ.DAL.Models
{
    /// <summary>
    ///     A model class that represents the structure of the table Log 
    /// </summary>
    public class Log
    {
        #region Properties

        /// <summary>
        ///     A guid property which has no default value.
        ///     This property is configured to be the primary key of Log table.
        ///     Will hold the id of log table.
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        ///     A string property which has a empty string as default value.
        ///     Will hold the value of the method name.
        /// </summary>
        public string MethodName { get; set; } = string.Empty;
        /// <summary>
        ///     A string property which has a empty string as default value.
        ///     Will hold the value of the method description.
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        ///     A guid property which is nullable.
        ///     Will hold the value of the id if the user.
        /// </summary>
        public Guid? UserId { get; set; }
        /// <summary>
        ///     A int property which will hold the id of the log type.
        /// </summary>
        public int LogTypeId { get; set; }
        /// <summary>
        ///     A navigation property pf <see cref="LogType"/> which supports lazy loading too.
        /// </summary>
        public virtual LogType? LogType { get; set; }

        #endregion
    }
}
#region Usings
using FAQ.DAL.BaseModels;
using System.ComponentModel.DataAnnotations;
#endregion

namespace FAQ.DAL.Models
{
    /// <summary>
    ///     A model class that represents the structure of the table Tag.
    ///     This model inherits from <see cref="BaseInheritable"/> and all its properties.
    /// </summary>
    public class Tag : BaseInheritable
    {
        #region Properties

        /// <summary>
        ///     A <see cref="Guid"/> property which has no default value.
        ///     This property is configured to be the primary key of Tag table.
        ///     Will hold the id of tag table.
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        ///     A <see cref="string"/> property which has a empty string as default value, <see cref="string.Empty"/>.
        ///     This property is configured to be required/not null using <see cref="RequiredAttribute"/>.
        ///     Will hold the value of the tag name.
        /// </summary>
        [Required]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        ///     A property used for inhertiance purposes with <see cref="QuestionTag"/> model and lazy loading.
        ///     Will hold the QuestionTags of a user.
        ///     This property it's nullable.
        /// </summary>
        public virtual ICollection<QuestionTag>? QuestionTags { get; set; }

        #endregion
    }
}
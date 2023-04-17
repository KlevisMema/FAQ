#region Usings
using FAQ.DAL.BaseModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#endregion

namespace FAQ.DAL.Models
{
    /// <summary>
    ///     A model class that represents the structure of the table Question 
    ///     and inherits from <see cref="BaseUserInheritable"/> and all its properties.
    /// </summary>
    public class Question : BaseUserInheritable
    {
        #region Properties

        /// <summary>
        ///     A <see cref="Guid"/> property which has no default value.
        ///     This property is configured to be the primary key of Question table using <see cref="KeyAttribute"/>.
        ///     Will hold the id of question table.
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        ///     A <see cref="string"/> property which has a empty string 
        ///     as default value, <see cref="string.Empty"/>. It configured 
        ///     to not be null by adding <see cref="RequiredAttribute"/>, and has a 
        ///     lengs of max characters of 15 using <see cref="StringLengthAttribute"/>.
        ///     This prop will hold the tittle of the question.
        /// </summary>
        [Required]
        [StringLength(15)]
        public string Tittle { get; set; } = string.Empty;
        /// <summary>
        ///     A string property which has a empty string as default value, <see cref="string.Empty"/>.
        ///     This property is configured to be required/not null using <see cref="RequiredAttribute"/>, 
        ///     and the clumn name as "Question" using <see cref="ColumnAttribute"/>. Will hold the value of the question description.
        /// </summary>
        [Required]
        [Column("Question")]
        public string P_Question { get; set; } = string.Empty;
        /// <summary>
        ///     A property used for inhertiance purposes with <see cref="Answer"/> model and lazy loading.
        ///     Will hold the Answers of a user.
        ///     It's  nullable.
        /// </summary>
        public virtual ICollection<Answer>? Answers { get; set; }
        /// <summary>
        ///     A property used for inhertiance purposes with <see cref="QuestionTag"/> model and lazy loading.
        ///     Will hold the QuestionTags of a user.
        ///     It's  nullable.
        /// </summary>
        public virtual ICollection<QuestionTag>? QuestionTags { get; set; }

        #endregion
    }
}
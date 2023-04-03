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
        ///     A guid property which has no default value.
        ///     This property is configured to be the primary key of Question table.
        ///     Will hold the id of question table.
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        ///     A string property which has a empty string as default value.
        ///     This property is configured to be required/not null and the clumn name as "Question".
        ///     Will hold the value of the question description.
        /// </summary>
        [Required]
        [Column("Question")]
        public string P_Question { get; set; } = string.Empty;
        /// <summary>
        ///     A property used for inhertiance purposes with <see cref="Answer"/> model and lazy loading.
        ///     Will hold the Answers of a user.
        /// </summary>
        public virtual ICollection<Answer>? Answers { get; set; }
        /// <summary>
        ///     A property used for inhertiance purposes with <see cref="QuestionTag"/> model and lazy loading.
        ///     Will hold the QuestionTags of a user.
        /// </summary>
        public virtual ICollection<QuestionTag>? QuestionTags { get; set; }

        #endregion
    }
}
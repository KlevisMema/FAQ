#region Usings
using FAQ.DAL.BaseModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#endregion

namespace FAQ.DAL.Models
{
    /// <summary>
    ///     A model class that represents the structure of the table Answer 
    ///     and inherits from <see cref="BaseUserInheritable"/> and all its properties.
    /// </summary>
    public class Answer : BaseUserInheritable
    {
        #region Properties

        /// <summary>
        ///     A <see cref="Guid"/> property which has no default value.
        ///     This property is configured to be the primary key of Answer table using <see cref="KeyAttribute"/>.
        ///     Will hold the id of answer table.
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        ///     A <see cref="string"/> property which has a empty string as default value, <see cref="string.Empty"/>.
        ///     This property is configured to be required/not null using <see cref="RequiredAttribute"/>,
        ///     and the clumn name as "Answer" using using <see cref="ColumnAttribute"/>.
        ///     Will hold the value of the answer of a question.
        ///  </summary>
        [Required]
        [Column("Answer")]
        public string P_Answer { get; set; } = string.Empty;
        /// <summary>
        ///     A <see cref="Guid"/> property which has no default value but is nullable.
        ///     This property will hold the id of the parent answer.
        /// </summary>
        public Guid? ParentAnswerId { get; set; }
        /// <summary>
        ///     A <see cref="Answer"/> navigation property which has no default value but is nullable.
        ///     Used for self referencing.
        /// </summary>
        public Answer? ParentAnswer { get; set; }
        /// <summary>
        ///     A <see cref="ICollection{T}"/> where T is <see cref="Answer"/> property used for slef referencing answers.
        ///     Its nullable.
        /// </summary>
        public ICollection<Answer>? ChildAnswers { get; set; }
        /// <summary>
        ///     A <see cref="Guid"/> property which will hold the id of the question.
        /// </summary>
        public Guid? QuestionId { get; set; }
        /// <summary>
        ///     A navigation property of <see cref="Question"/>.
        ///     Its nullable.
        /// </summary>
        public virtual Question? Question { get; set; }

        #endregion
    }
}
#region Usings
using System.ComponentModel.DataAnnotations;
#endregion

namespace FAQ.DTO.AnswerDtos
{
    /// <summary>
    ///     A dto of the <see cref="FAQ.DAL.Models.Answer"/> model.
    ///     Used for creation of a new asnwer of an answer.
    /// </summary>
    public class DtoCreateAnswer
    {
        #region Properties
        /// <summary>
        ///     The id of the question.
        ///     This property is required.
        ///     It's marked as required/not null using <see cref="RequiredAttribute"/>.
        /// </summary>
        [Required]
        public Guid QuestionId { get; set; }
        /// <summary>
        ///     The answer content/text.
        ///     This property is required.
        ///     It's marked as required/not null using <see cref="RequiredAttribute"/>.
        /// </summary>
        [Required]
        public string Answer { get; set; } = string.Empty;
        #endregion
    }
}
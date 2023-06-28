#region Usings
using System.ComponentModel.DataAnnotations;
#endregion

namespace FAQ.DTO.AnswerDtos
{
    /// <summary>
    ///     A dto of the <see cref="FAQ.DAL.Models.Answer"/> model.
    ///     Used for creation of a new asnwer of an answer.
    /// </summary>
    public class DtoAnswerOfAnswer
    {
        #region Properties
        /// <summary>
        ///     The id of the question.
        ///     This property is required.
        /// </summary>
        [Required]
        public Guid QuestionId { get; set; }
        /// <summary>
        ///     The parent answer id.
        ///     This property is required.
        /// </summary>
        [Required]
        public Guid ParentAnswerId { get; set; }
        /// <summary>
        ///     The answer content/text.
        ///     This property is required.
        /// </summary>
        [Required]
        public string Answer { get; set; } = string.Empty;
        #endregion
    }
}
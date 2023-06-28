using System.ComponentModel.DataAnnotations;

namespace FAQ.DTO.AnswerDtos
{
    /// <summary>
    ///     A dto of the <see cref="FAQ.DAL.Models.Answer"/> model.
    ///     This dto is used in a form to edit a answer.
    /// </summary>
    public class DtoEditAnswer
    {
        #region Properties
        /// <summary>
        ///     The id of the answer.
        ///     This field is required.
        ///     It's marked as required/not null using <see cref="RequiredAttribute"/>.
        /// </summary>
        [Required]
        public Guid Id { get; set; }
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

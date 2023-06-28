#region Usings
using System.ComponentModel.DataAnnotations;
#endregion

namespace FAQ.DTO.QuestionsDtos
{
    /// <summary>
    ///     A dto of the <see cref="FAQ.DAL.Models.Question"/> model.
    ///     This dto is used in creating a new question.
    /// </summary>
    public class DtoCreateQuestion
    {
        #region Properties
        /// <summary>
        ///     The question text/content.
        ///     This property is required.
        ///     It's marked as required/not null using <see cref="RequiredAttribute"/>.
        /// </summary>
        [Required]
        public string P_Question { get; set; } = string.Empty;
        /// <summary>
        ///     The tittle of the question.
        ///     This property is required.
        ///     It's marked as required/not null using <see cref="RequiredAttribute"/>.
        /// </summary>
        [Required]
        public string Tittle { get; set; } = string.Empty;
        /// <summary>
        ///     The id of the tag.
        ///     This property is required.
        ///     It's marked as required/not null using <see cref="RequiredAttribute"/>.
        /// </summary>
        [Required]
        public Guid TagId { get; set; }
        #endregion
    }
}
#region Usings
using System.ComponentModel.DataAnnotations; 
#endregion

namespace FAQ.DTO.QuestionsDtos
{
    /// <summary>
    ///     A dto of the <see cref="FAQ.DAL.Models.Question"/> model.
    /// </summary>
    public class DtoUpdateQuestion
    {
        #region Properties
        /// <summary>
        ///     The id of the question.
        ///     This field is required.
        ///     It's marked as required/not null using <see cref="RequiredAttribute"/>.
        /// </summary>
        [Required]
        public Guid Id { get; set; }
        /// <summary>
        ///     The tittle of the question.
        ///     This field is required.
        ///     It's marked as required/not null using <see cref="RequiredAttribute"/>.
        /// </summary>
        [Required]
        public string Tittle { get; set; } = string.Empty;
        /// <summary>
        ///     The question text/content.
        ///     This field is required.
        ///     It's marked as required/not null using <see cref="RequiredAttribute"/>.
        /// </summary>
        [Required]
        public string P_Question { get; set; } = string.Empty;
        #endregion
    }
}
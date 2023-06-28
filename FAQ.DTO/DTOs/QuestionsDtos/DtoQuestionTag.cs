namespace FAQ.DTO.QuestionsDtos
{
    /// <summary>
    ///     A dto of the <see cref="FAQ.DAL.Models.Question"/> model.
    /// </summary>
    public class DtoQuestionTag
    {
        #region Properties
        /// <summary>
        ///     The id of the tag.
        /// </summary>
        public Guid TagId { get; set; }
        /// <summary>
        ///     The tag name text/content
        /// </summary>
        public string TagName { get; set; } = string.Empty;
        #endregion
    }
}
namespace FAQ.DTO.QuestionsDtos
{
    /// <summary>
    ///     A dto of the <see cref="FAQ.DAL.Models.Question"/> model.
    /// </summary>
    public class DtoCreateQuestionReturn
    {
        #region Properties
        /// <summary>
        ///     The id of the question.
        /// </summary>
        public Guid QuestionId { get; set; }
        /// <summary>
        ///     The id of the tag.
        /// </summary>
        public Guid TagId { get; set; }
        /// <summary>
        ///     The tittle of the question.
        /// </summary>
        public string Tittle { get; set; } = string.Empty;
        /// <summary>
        ///     The question text/content.
        /// </summary>
        public string P_Question { get; set; } = string.Empty;
        #endregion
    }
}
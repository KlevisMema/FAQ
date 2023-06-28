namespace FAQ.DTO.QuestionsDtos
{
    /// <summary>
    ///     A dto of the <see cref="FAQ.DAL.Models.Question"/> model.
    /// </summary>
    public class DtoDisabledQuestion
    {
        #region Properties
        /// <summary>
        ///     The id of the question.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        ///     The question text/content.
        /// </summary>
        public string P_Question { get; set; } = string.Empty;
        /// <summary>
        ///     Time when question was deleted.
        /// </summary>
        public DateTime DeletedAt { get; set; }
        /// <summary>
        ///     The tittle of the question.
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        ///     State of the question.
        /// </summary>
        public bool Disabled { get; set; }
        /// <summary>
        ///     A <see cref="List{T}"/> where T => <see cref="DtoQuestionTag"/>.
        /// </summary>
        public List<DtoQuestionTag>? DtoQuestionTags { get; set; }
        #endregion
    }
}
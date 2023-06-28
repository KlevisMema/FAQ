namespace FAQ.DTO.QuestionsDtos
{
    /// <summary>
    ///     A dto of the <see cref="FAQ.DAL.Models.Question"/> model.
    /// </summary>
    public class DtoDeletedQuestion
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
        #endregion
    }
}
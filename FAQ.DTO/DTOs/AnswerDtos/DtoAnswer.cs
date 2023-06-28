namespace FAQ.DTO.AnswerDtos
{
    /// <summary>
    ///     A dto of the <see cref="FAQ.DAL.Models.Answer"/> model.
    /// </summary>
    public class DtoAnswer
    {
        #region Properties
        /// <summary>
        ///     The id of the answer.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        ///     The answer content/text.
        /// </summary>
        public string Answer { get; set; } = string.Empty;
        /// <summary>
        ///     A <see cref="List{T}"/> where T => <see cref="DtoAnswer"/>.
        ///     It's a list of child answers of this answer.
        /// </summary>
        public List<DtoAnswer>? ChildAnswers { get; set; }
        #endregion
    }
}
namespace FAQ.DTO.AnswerDtos
{
    /// <summary>
    ///     A dto of the <see cref="FAQ.DAL.Models.Answer"/> model.
    /// </summary>
    public class DtoGetAnswer
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
        #endregion
    }
}
namespace FAQ.DAL.Models
{
    /// <summary>
    ///     A model class designed to be a table that holds a relationship of many to many 
    ///     between <see cref="Question"/> and <see cref="Tag"/> and represent a table 
    ///     called QuestionTag.
    /// </summary>
    public class QuestionTag
    {
        #region Properties

        /// <summary>
        ///     A guid property which will hold the question id.
        /// </summary>
        public Guid QuestionId { get; set; }
        /// <summary>
        ///     A guid property which will hold the tag id.
        /// </summary>
        public Guid TagId { get; set; }
        /// <summary>
        ///     A navigation property for question, nullable.
        /// </summary>
        public Question? Question { get; set; }
        /// <summary>
        ///     A navigation property for Tag, nullable.
        /// </summary>
        public Tag? Tag { get; set; }

        #endregion
    }
}
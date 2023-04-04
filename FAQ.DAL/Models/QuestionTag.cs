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
        ///     A <see cref="Guid"/> property which will hold the question id.
        /// </summary>
        public Guid QuestionId { get; set; }
        /// <summary>
        ///     A <see cref="Guid"/> property which will hold the tag id.
        /// </summary>
        public Guid TagId { get; set; }
        /// <summary>
        ///     A navigation property for <see cref="Question"/>, it's nullable.
        /// </summary>
        public Question? Question { get; set; }
        /// <summary>
        ///     A navigation property for <see cref="Tag"/>, it's nullable.
        /// </summary>
        public Tag? Tag { get; set; }

        #endregion
    }
}
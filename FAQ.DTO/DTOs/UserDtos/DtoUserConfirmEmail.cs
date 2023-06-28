namespace FAQ.DTO.UserDtos
{
    /// <summary>
    ///     A dto used to be passed to the email service. 
    /// </summary>
    public class DtoUserConfirmEmail
    {
        #region Properties

        /// <summary>
        ///     A <see cref="Guid"/> property type, 
        ///     it will hold the id of a user.
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        ///     A <see cref="string"/> property type, it will hold 
        ///     the email of a user. It's configured to have a empty 
        ///     strig as a default value.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        #endregion
    }
}
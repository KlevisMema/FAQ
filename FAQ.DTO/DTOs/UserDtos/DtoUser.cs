namespace FAQ.DTO.UserDtos
{
    /// <summary>
    ///     A dto used to be passed to the OAuthService.
    /// </summary>
    public class DtoUser
    {
        #region Properties

        /// <summary>
        ///     A <see cref="string"/> property, it's configured to have a default value an empty string.
        ///     it will hold the id of a user.
        /// </summary>
        public string Id { get; set; } = string.Empty;
        /// <summary>
        ///     A <see cref="string"/> property, it's configured to have a default value an empty string.
        ///     it will hold the email of a user.
        /// </summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>
        ///     A <see cref="List{T}"/> where T  is <see cref="string"/> property, it's configured to have a default value a empty list, 
        ///     <see cref="Enumerable.Empty{TResult}"/> where TResult is <see cref="string"/>.
        ///     It will hold roles of a user.
        /// </summary>
        public List<string> Roles { get; set; } = Enumerable.Empty<string>().ToList();

        #endregion
    }
}
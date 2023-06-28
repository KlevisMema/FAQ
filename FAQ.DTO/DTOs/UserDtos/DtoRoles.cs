namespace FAQ.DTO.UserDtos
{
    /// <summary>
    ///     A dto used for roles.
    /// </summary>
    public class DtoRoles
    {
        #region Properties

        /// <summary>
        ///     A <see cref="string"/> property, it's configured to have a default value an empty string.
        ///     it will hold the id of the role.
        /// </summary>
        public string Id { get; set; } = string.Empty;
        /// <summary>
        ///     A <see cref="string"/> property, it's configured to have a default value an empty string.
        ///     it will hold the name of the role.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        #endregion
    }
}
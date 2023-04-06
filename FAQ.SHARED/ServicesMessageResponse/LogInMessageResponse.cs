namespace FAQ.SHARED.ServicesMessageResponse
{
    /// <summary>
    ///     A class that represents the LogInMessageResponse section in appsettings.json.
    ///     Its used to get the section values from it and used outside the API project assembly.
    /// </summary>
    public class LogInMessageResponse
    {
        #region Properties
        /// <summary>
        ///     A <see cref="string"/> prop that holds the value of the UserNotFound section.
        ///     Default value <see cref="string.Empty"/>
        /// </summary>
        public string UserNotFound { get; set; } = string.Empty;
        /// <summary>
        ///     A <see cref="string"/> prop that holds the value of the UnconfirmedEmail section.
        ///     Default value <see cref="string.Empty"/>
        /// </summary>
        public string UnconfirmedEmail { get; set; } = string.Empty;
        /// <summary>
        ///     A <see cref="string"/> prop that holds the value of the UserNoRoles section.
        ///     Default value <see cref="string.Empty"/>
        /// </summary>
        public string UserNoRoles { get; set; } = string.Empty;
        /// <summary>
        ///     A <see cref="string"/> prop that holds the value of the InvalidCredentials section.
        ///     Default value <see cref="string.Empty"/>
        /// </summary>
        public string InvalidCredentials { get; set; } = string.Empty;
        #endregion
    }
}
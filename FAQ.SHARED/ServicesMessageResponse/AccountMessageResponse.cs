namespace FAQ.SHARED.ServicesMessageResponse
{
    /// <summary>
    ///     A class that represents the AccountMessageResponse section in appsettings.json.
    ///     Its used to get the section values from it and used outside the API project assembly.
    /// </summary>
    public class AccountMessageResponse
    {
        #region Properties
        /// <summary>
        ///     A <see cref="string"/> prop that holds the value of the OtpEmpty section.
        ///     Default value <see cref="string.Empty"/>
        /// </summary>
        public string OtpEmpty { get; set; } = string.Empty;
        /// <summary>
        ///     A <see cref="string"/> prop that holds the value of the UserNotFound section.
        ///     Default value <see cref="string.Empty"/>
        /// </summary>
        public string UserNotFound { get; set; } = string.Empty;
        /// <summary>
        ///     A <see cref="string"/> prop that holds the value of the OtpCodeIncorrect section.
        ///     Default value <see cref="string.Empty"/>
        /// </summary>
        public string OtpCodeIncorrect { get; set; } = string.Empty;
        /// <summary>
        ///     A <see cref="string"/> prop that holds the value of the AccountConfirmed section.
        ///     Default value <see cref="string.Empty"/>
        /// </summary>
        public string AccountConfirmed { get; set; } = string.Empty; 
        #endregion
    }
}
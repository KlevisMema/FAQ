namespace FAQ.SHARED.ServicesMessageResponse
{
    /// <summary>
    ///     A container class that represents the ServiceMessageResponseContainer section in appsettings.json.
    ///     Its used to get the section values from it and used outside the API project assembly.
    ///     It will hold all the nested sections in it.
    /// </summary>
    public class ServiceMessageResponseContainer
    {
        #region Properties
        /// <summary>
        ///     The name of the section.
        /// </summary>
        public const string SectionName = "ServiceMessageResponse";
        /// <summary>
        ///     Child secyion <see cref="RegisterMessageResponse"/> that will
        ///     hold all the values of it's own section.
        /// </summary>
        public RegisterMessageResponse? RegisterMessageResponse { get; set; }
        /// <summary>
        ///     Child secyion <see cref="LogInMessageResponse"/> that will
        ///     hold all the values of it's own section.
        /// </summary>
        public LogInMessageResponse? LogInMessageResponse { get; set; }
        /// <summary>
        ///     Child secyion <see cref="AccountMessageResponse"/> that will
        ///     hold all the values of it's own section.
        /// </summary>
        public AccountMessageResponse? AccountMessageResponse { get; set; }
        /// <summary>
        ///     A <see cref="string"/> prop that holds the value of the Exception section.
        ///     Default value <see cref="string.Empty"/>
        /// </summary>
        public string Exception { get; set; } = string.Empty;
        #endregion
    }
}
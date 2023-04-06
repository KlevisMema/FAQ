namespace FAQ.SHARED.ServicesMessageResponse
{
    /// <summary>
    ///     A class that represents the RegisterMessageResponse section in appsettings.json.
    ///     Its used to get the section values from it and used outside the API project assembly.
    /// </summary>
    public class RegisterMessageResponse
    {
        #region Properties
        /// <summary>
        ///     A <see cref="string"/> prop that holds the value of the FailRegistration section.
        ///     Default value <see cref="string.Empty"/>
        /// </summary>
        public string FailRegistration { get; set; } = string.Empty;
        /// <summary>
        ///     A <see cref="string"/> prop that holds the value of the SuccsessRegistration section.
        ///     Default value <see cref="string.Empty"/>
        /// </summary>
        public string SuccsessRegistration { get; set; } = string.Empty;
        /// <summary>
        ///     A <see cref="string"/> prop that holds the value of the SaveSuccsessRegistrationLog section.
        ///     Default value <see cref="string.Empty"/>
        /// </summary>
        public string SaveSuccsessRegistrationLog { get; set; } = string.Empty;
        #endregion
    }
}
namespace FAQ.ACCOUNT.AuthenticationService
{
    /// <summary>
    ///     Class used to desirialize the values from appsettings.json.
    ///     This class represents the structure of the section of Jwt section,
    ///     which is configured in the FAQ.API/Startup/ProgramExtension
    /// </summary>
    public class AuthenticationSettings
    {
        /// <summary>
        ///     The name of the section 
        /// </summary>
        public const string SectionName = "Jwt";
        /// <summary>
        ///      Audience prop which value is stored in appsettings.json, default value empty string ""
        /// </summary>
        public string Audience { get; set; } = string.Empty;
        /// <summary>
        ///     Key prop which value is stored in appsettings.json, default value empty string ""
        /// </summary>
        public string Key { get; set; } = string.Empty;
        /// <summary>
        ///    Issuer prop string which value is stored in appsettings.json, default value empty string ""
        /// </summary>
        public string Issuer { get; set; } = string.Empty;
        /// <summary>
        ///     LifeTime prop integer which value is stored in appsettings.json, default value 0
        /// </summary>
        public int LifeTime { get; set; } = 0;
    }
}
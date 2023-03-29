namespace FAQ.SERVICES.AuthenticationService
{
    /// <summary>
    ///     Class used to desirialize the values from a json file
    /// </summary>
    public class AuthenticationSettings
    {
        /// <summary>
        ///      Audience string which value is stored in a json file
        /// </summary>
        public string Audience { get; set; } = string.Empty;
        /// <summary>
        ///     Key string which value is stored in a json file
        /// </summary>
        public string Key { get; set; } = string.Empty;
        /// <summary>
        ///    Issuer string which value is stored in a json filek
        /// </summary>
        public string Issuer { get; set; } = string.Empty;
        /// <summary>
        ///     LifeTime integer which value is stored in a json file
        /// </summary>
        public int LifeTime { get; set; } = 0;
    }
}
namespace FAQ.EMAIL.EmailService
{
    /// <summary>
    ///     A Class that represents settings stored in the appsettings.json which is configured in ProgramExtension to take the values ad passed to each prop of this class.
    /// </summary>
    public class EmailSettings
    {
        /// <summary>
        ///     From who is the email sent
        /// </summary>
        public string From { get; set; } = string.Empty;
        /// <summary>
        ///     Smtp server
        /// </summary>
        public string SmtpServer { get; set; } = string.Empty;
        /// <summary>
        ///     Stmp server username
        /// </summary>
        public string SmtpUsername { get; set; } = string.Empty;
        /// <summary>
        ///     Stp server password
        /// </summary>
        public string SmtpPassword { get; set; } = string.Empty;
        /// <summary>
        ///     Smtp server port
        /// </summary>
        public int Port { get; set; } = 0;
        /// <summary>
        ///     Smtp server use ssl
        /// </summary>
        public bool UseSSL { get; set; } = false;
        /// <summary>
        ///     Stmp server is body html
        /// </summary>
        public bool IsBodyHtml { get; set; } = false;
    }
}
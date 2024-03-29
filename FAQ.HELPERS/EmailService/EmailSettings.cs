﻿namespace FAQ.EMAIL.EmailService
{
    /// <summary>
    ///     A Class that represents settings stored in the appsettings.json which is configured in ProgramExtension to take the values ad passed to each prop of this class.
    /// </summary>
    public class EmailSettings
    {
        #region Properties
        /// <summary>
        ///     The name of the section 
        /// </summary>
        public const string SectionName = "EmailSettings";
        /// <summary>
        ///     From who is the email sent.
        ///     It has a default value <see cref="string.Empty"/>.
        /// </summary>
        public string From { get; set; } = string.Empty;
        /// <summary>
        ///     Smtp server.
        ///     It has a default value <see cref="string.Empty"/>.
        /// </summary>
        public string SmtpServer { get; set; } = string.Empty;
        /// <summary>
        ///     Stmp server username.
        ///     It has a default value <see cref="string.Empty"/>.
        /// </summary>
        public string SmtpUsername { get; set; } = string.Empty;
        /// <summary>
        ///     Smtp server password.
        ///     It has a default value <see cref="string.Empty"/>.
        /// </summary>
        public string SmtpPassword { get; set; } = string.Empty;
        /// <summary>
        ///     Smtp server port.
        ///     It has a default value of 0.
        /// </summary>
        public int Port { get; set; } = 0;
        /// <summary>
        ///     Smtp server use ssl.
        ///     It has a default value <see langword="false"/>.
        /// </summary>
        public bool UseSSL { get; set; } = false;
        /// <summary>
        ///     Stmp server is body html.
        ///     It has a default value <see langword="false"/>.
        /// </summary>
        public bool IsBodyHtml { get; set; } = false;
        /// <summary>
        ///     Stmp server host.
        ///     It has a default value <see cref="string.Empty"/>.
        /// </summary>
        public string Host { get; set; } = string.Empty;
        /// <summary>
        ///     Email Subject.
        ///     It has a default value <see cref="string.Empty"/>.
        /// </summary>
        public string Subject { get; set; } = string.Empty;
        /// <summary>
        ///     Email body.
        ///     It has a default value <see cref="string.Empty"/>.
        /// </summary>
        public string Body { get; set; } = string.Empty;
        #endregion
    }
}
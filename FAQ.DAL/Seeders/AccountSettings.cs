namespace FAQ.DAL.Seeders
{
    /// <summary>
    ///     A class providing all the settings required for the user seeding, values stored and
    ///     retrieved from the FAQ.API/appsettings.json.
    ///     This class repesents the structure of the section of "Users" which is configured
    ///     in the FAQ.API/Startup/ProgramExtension
    /// </summary>
    public class AccountSettings
    {
        /// <summary>
        ///     A constant property that has the section name in the appsettings.json.
        /// </summary>
        public const string SectionName = "Users";
        /// <summary>
        ///     A property that will hold the value of the username, 
        ///     default value empty string.
        /// </summary>
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        ///     A property that will hold the value of the password,
        ///     default value empty string.
        /// </summary>
        public string Password { get; set; } = string.Empty;
        /// <summary>
        ///     A property that will hold the value of the name, 
        ///     default value empty string.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        ///     A property that will hold the value of the surname, 
        ///     default value empty string.
        /// </summary>
        public string SurnName { get; set; } = string.Empty;
        /// <summary>
        ///     A property that will hold the value of the adress, 
        ///     default value empty string.
        /// </summary>
        public string Adress { get; set; } = string.Empty;
        /// <summary>
        ///     A property that will hold the value of the is admin, 
        ///     default value empty string.
        /// </summary>
        public bool IsAdmin { get; set; } = false;
        /// <summary>
        ///     A property that will hold the value of the username, 
        ///     default value false.
        /// </summary>
        public int Age { get; set; } = 0;
        /// <summary>
        ///     A property that will hold the value of the username, 
        ///     default value a empty array.
        /// </summary>
        public string[] Roles { get; set; } = new string[0];
    }
}
namespace FAQ.SHARED.ServicesMessageResponse
{
    /// <summary>
    ///     A class that represents the UploadProfilePictureMessageResponse section in appsettings.json.
    ///     Its used to get the section values from it and used outside the API project assembly.
    /// </summary>
    public class UploadProfilePictureMessageResponse
    {
        #region Properties
        /// <summary>
        ///     A <see cref="string"/> prop that holds the value of the ProfilePicNull section.
        ///     Default value <see cref="string.Empty"/>
        /// </summary>
        public string ProfilePicNull { get; set; } = string.Empty;
        /// <summary>
        ///     A <see cref="string"/> prop that holds the value of the Unsuccsessfull section.
        ///     Default value <see cref="string.Empty"/>
        /// </summary>
        public string Unsuccsessfull { get; set; } = string.Empty;
        /// <summary>
        ///     A <see cref="string"/> prop that holds the value of the Succsessfull section.
        ///     Default value <see cref="string.Empty"/>
        /// </summary>
        public string Succsessfull { get; set; } = string.Empty; 
        #endregion
    }
}
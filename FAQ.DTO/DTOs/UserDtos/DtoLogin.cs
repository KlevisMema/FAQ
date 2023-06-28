#region Usings
using System.ComponentModel.DataAnnotations;
#endregion

namespace FAQ.DTO.UserDtos
{
    /// <summary>
    ///     A dto used for login.
    /// </summary>
    public class DtoLogin
    {
        #region Properties

        /// <summary>
        ///     A string property marked as required/not null using <see cref="RequiredAttribute"/>, default value an empty string.
        ///      And its  marked as an  email adress using <see cref="EmailAddressAttribute"/> so this property will require an email addres format or otherwise will be empty.
        /// </summary>
        [Required(ErrorMessage = "Email field is required ")]
        [EmailAddress(ErrorMessage = "Email is not a vaild email format")]
        public string Email { get; set; } = string.Empty;
        /// <summary>
        ///     A string property marked as required/not null using <see cref="RequiredAttribute"/>, default value an empty string.
        ///     This property is formated as a password format using <see cref="DataType.Password"/>.
        ///     Its configured to  have a length of max 30 characters including white spaces and min of 6 charactes with custom Error message, using <see cref="StringLengthAttribute"/>.
        ///     it will hold the value of the user trying to log in.
        /// </summary>
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password field is required")]
        [StringLength(maximumLength: 30, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; } = string.Empty;

        #endregion
    }
}
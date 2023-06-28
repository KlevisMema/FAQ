#region Usings
using FAQ.SHARED.Enums;
using System.ComponentModel.DataAnnotations;
#endregion

namespace FAQ.DTO.UserDtos
{
    /// <summary>
    ///     A dto used for registering a user.
    ///     This  dto class represents the user table but only 
    ///     with the necessary properties.
    /// </summary>
    public class DtoRegister
    {
        #region Properties 

        /// <summary>
        ///     A <see cref="string"/> property marked as required/not null using <see cref="RequiredAttribute"/>, default value an empty string.
        ///     And its  marked as an  email adress using <see cref="EmailAddressAttribute"/> so this property will require an email addres format or otherwise will be empty.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        /// <summary>
        ///     A <see cref="string"/> property marked as required/not null using <see cref="RequiredAttribute"/>, default value an empty string.
        ///     It will hold the first name of registering user.
        /// </summary>
        [Required]
        public string FirstName { get; set; } = string.Empty;
        /// <summary>
        ///     A <see cref="string"/> property marked as required/not null using <see cref="RequiredAttribute"/>, default value an empty string.
        ///     It will hold the last name of registering user.
        /// </summary>
        [Required]
        public string LastName { get; set; } = string.Empty;
        /// <summary>
        ///     A <see cref="Enum"/> property marked as required/not null using <see cref="RequiredAttribute"/>.
        ///     Default value is an empty string.
        ///     It will hold the gender of registering user.
        /// </summary>
        [Required]
        public Gender Gender { get; set; }
        /// <summary>
        ///     A <see cref="int"/> property marked as required/not null using <see cref="RequiredAttribute"/>.
        ///     It has 0 as a  default value.
        ///     It will hold the age of registering user.
        /// </summary>
        [Required]
        public int Age { get; set; } = 0;
        /// <summary>
        ///     A <see cref="DateTime"/> property marked as required/not null using <see cref="RequiredAttribute"/>.
        ///     It will hold the birth day of registering user <see cref="DataType.Date"/>.
        ///     Its formatted in this  format ex : 04/04/2024 => day/month/year using <see cref="DisplayFormatAttribute"/>
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy}")]
        public DateTime DateOfBirth { get; set; }
        /// <summary>
        ///     A <see cref="string"/> property marked as required/not null using <see cref="RequiredAttribute"/>, with a default value as an empty string.
        ///     It will hold the password of registering user.
        ///     Its formatted in password format with <see cref="DataType.Password"/>.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        /// <summary>
        ///     A <see cref="string"/> property marked as required/not null using <see cref="RequiredAttribute"/>, with a default value as an empty string.
        ///     It will hold the confirm password of registering user.
        ///     Its formatted in password format with <see cref="DataType.Password"/>.
        ///     This field has the should have the same value as <see cref="Password"/> field, 
        ///     this will be checked by <see cref="CompareAttribute"/>.
        /// </summary>
        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string PasswordConfirmation { get; set; } = string.Empty;
        /// <summary>
        ///     A <see cref="string"/> property marked as required/not null using <see cref="RequiredAttribute"/>, with a default value as an empty string.
        ///     It will hold the phone prefix of registering user.
        /// </summary>
        [Required]
        public string PhonePrefix { get; set; } = string.Empty;
        /// <summary>
        ///     A <see cref="string"/> property marked as required/not null using <see cref="RequiredAttribute"/>, with a default value as an empty string.
        ///     It will hold the phone number of registering user.
        ///     It has the <see cref="PhoneAttribute"/>, so this property will require as a phone number as a format.
        /// </summary>
        [Phone]
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
        /// <summary>
        ///     A <see cref="string"/> property marked as required/not null using <see cref="RequiredAttribute"/>, with a default value as an empty string.
        ///     It will hold the adress of registering user.
        /// </summary>
        [Required]
        public string Adress { get; set; } = string.Empty;

        #endregion
    }
}
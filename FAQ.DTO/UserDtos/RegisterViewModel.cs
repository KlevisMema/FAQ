using FAQ.SHARED.Enums;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace FAQ.DTO.UserDtos
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public Gender Gender { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy}")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Compare("Password")]
        public string PasswordConfirmation { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
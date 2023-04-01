using FAQ.SHARED.Enums;
using System.ComponentModel.DataAnnotations;

namespace FAQ.DTO.UserDtos
{
    public class DtoRegister
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
        public int Age { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy}")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string PasswordConfirmation { get; set; } = string.Empty;

        [Required]
        public string PhonePrefix { get; set; } = string.Empty;

        [Phone]
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
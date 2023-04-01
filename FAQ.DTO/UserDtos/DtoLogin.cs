using System.ComponentModel.DataAnnotations;

namespace FAQ.DTO.UserDtos
{
    public class DtoLogin
    {
        [Required(ErrorMessage = "Email field is required ")]
        [EmailAddress(ErrorMessage = "Email is not a vaild email format")]
        public string Email { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password field is required")]
        [StringLength(maximumLength: 30, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; } = string.Empty;
    }
}
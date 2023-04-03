namespace FAQ.DTO.UserDtos
{
    public class DtoUserConfirmEmail
    {
        public Guid UserId { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
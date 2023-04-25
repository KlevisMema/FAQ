using System.ComponentModel.DataAnnotations;

namespace FAQ.DTO.AnswerDtos
{
    public class DtoEditAnswer
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Answer { get; set; } = string.Empty;
    }
}

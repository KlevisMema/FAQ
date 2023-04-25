using System.ComponentModel.DataAnnotations;

namespace FAQ.DTO.AnswerDtos
{
    public class DtoAnswerOfAnswer
    {
        [Required]
        public Guid QuestionId { get; set; }
        [Required]
        public Guid ParentAnswerId { get; set; }
        [Required]
        public string Answer { get; set; } = string.Empty;
    }
}
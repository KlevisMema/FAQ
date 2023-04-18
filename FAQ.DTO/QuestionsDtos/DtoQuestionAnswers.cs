using FAQ.DTO.AnswerDtos;

namespace FAQ.DTO.QuestionsDtos
{
    public class DtoQuestionAnswers
    {
        public Guid Id { get; set; }
        public string P_Question { get; set; } = string.Empty;
        public DateTime? Created { get; set; }
        public DateTime? Edited { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool Disabled { get; set; }
        public List<DtoQuestionTag>? DtoQuestionTags { get; set; }
        public List<DtoAnswer>? DtoAnswers { get; set; }
    }
}
namespace FAQ.DTO.AnswerDtos
{
    public class DtoCreateAnswer
    {
        public Guid QuestionId { get; set; }
        public string Answer { get; set; } = string.Empty;
    }
}
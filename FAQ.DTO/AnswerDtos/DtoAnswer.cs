namespace FAQ.DTO.AnswerDtos
{
    public class DtoAnswer
    {
        public Guid Id { get; set; }
        public string Answer { get; set; } = string.Empty;
        public List<DtoAnswer>? ChildAnswers { get; set; }
    }
}
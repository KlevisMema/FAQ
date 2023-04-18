namespace FAQ.DTO.QuestionsDtos
{
    public class DtoDisabledQuestion
    {
        public Guid Id { get; set; }
        public string P_Question { get; set; } = string.Empty;
        public DateTime DeletedAt { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool Disabled { get; set; }
        public List<DtoQuestionTag>? DtoQuestionTags { get; set; }
    }
}
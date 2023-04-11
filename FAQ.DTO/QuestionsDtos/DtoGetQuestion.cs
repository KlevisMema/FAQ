namespace FAQ.DTO.QuestionsDtos
{
    public class DtoGetQuestion
    {
        public Guid Id { get; set; }
        public string P_Question { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public DateTime Edited { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace FAQ.DTO.QuestionsDtos
{
    public class DtoCreateQuestionReturn
    {
        public Guid QuestionId { get; set; }
        public Guid TagId { get; set; }
        public string Tittle { get; set; } = string.Empty;
        public string P_Question { get; set; } = string.Empty;
    }
}
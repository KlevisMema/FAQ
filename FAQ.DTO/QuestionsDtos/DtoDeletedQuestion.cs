using System.ComponentModel.DataAnnotations;

namespace FAQ.DTO.QuestionsDtos
{
    public class DtoDeletedQuestion
    {
        public Guid Id { get; set; }
        public string P_Question { get; set; } = string.Empty;
        public DateTime DeletedAt { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace FAQ.DTO.QuestionsDtos
{
    public class DtoUpdateQuestion
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string P_Question { get; set; } = string.Empty;
        [Required]
        public string Tittle { get; set; } = string.Empty;
    }
}
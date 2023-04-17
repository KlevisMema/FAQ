#region Usings
using System.ComponentModel.DataAnnotations;
#endregion

namespace FAQ.DTO.QuestionsDtos
{
    public class DtoCreateQuestion
    {
        [Required]
        public string P_Question { get; set; } = string.Empty;
        [Required]
        public string Tittle { get; set; } = string.Empty;
        [Required]
        public Guid TagId { get; set; }
    }
}
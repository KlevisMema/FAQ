using FAQ.DAL.BaseModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FAQ.DAL.Models
{
    public class Question : BaseUserInheritancable
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [Column("Question")]
        public string P_Question { get; set; } = string.Empty;

        public virtual ICollection<Answer>? Answers { get; set; }

        public virtual ICollection<QuestionTag>? QuestionTags { get; set; }
    }
}
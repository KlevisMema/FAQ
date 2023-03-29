using FAQ.DAL.BaseModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FAQ.DAL.Models
{
    public class Answer : BaseUserInheritancable
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [Column("Answer")]
        public string P_Answer { get; set; } = string.Empty;

        public Guid? ParentAnswerId { get; set; }
        public Answer? ParentAnswer { get; set; }
        public ICollection<Answer>? ChildAnswers { get; set; }

        public Guid QuestionId { get; set; }
        public virtual Question? Question { get; set; }
    }
}
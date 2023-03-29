using FAQ.DAL.BaseModels;
using System.ComponentModel.DataAnnotations;

namespace FAQ.DAL.Models
{
    public class Tag : BaseInheritancable
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<QuestionTag>? QuestionTags { get; set; }
    }
}
using FAQ.SHARED.Enums;
using FAQ.DAL.BaseModels;
using System.ComponentModel.DataAnnotations;

namespace FAQ.DAL.Models
{
    public class User : BaseIdentityInheritancable
    {
        [Required]
        [StringLength(50)]
        public string? Name { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string? Surname { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public Gender? Gender { get; set; }

        [Required]
        [Range(maximum: 150, minimum: 0)]
        public int Age { get; set; } = 0;

        [Required]
        [StringLength(100)]
        public string? Adress { get; set; } = string.Empty;

        [Required]
        [StringLength(4)]
        public string? Prefix { get; set; } = string.Empty;

        public virtual ICollection<Question>? Questions { get; set; }
        public virtual ICollection<Answer>? Answers { get; set; }
    }
}
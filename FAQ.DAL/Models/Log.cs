using System.ComponentModel.DataAnnotations;

namespace FAQ.DAL.Models
{
    public class Log
    {
        [Key]
        public int Id { get; set; }

        public string MethodName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid? UserId { get; set; } 
        public int LogTypeId { get; set; }
        public virtual LogType? LogType { get; set; }
    }
}
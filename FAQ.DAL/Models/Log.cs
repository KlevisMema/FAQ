using System.ComponentModel.DataAnnotations;

namespace FAQ.DAL.Models
{
    public class Log
    {
        [Key]
        public Guid Id { get; set; }

        public string MethodName { get; set; } = string.Empty;
        public string Exception { get; set; } = string.Empty;
        public Guid? UserId { get; set; } 
        public Guid LogTypeId { get; set; }
        public virtual LogType? LogType { get; set; }
       
    }
}
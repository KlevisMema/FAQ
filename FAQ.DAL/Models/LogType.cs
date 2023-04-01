﻿using System.ComponentModel.DataAnnotations;

namespace FAQ.DAL.Models
{
    public class LogType
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Log>? Logs { get; set; } 
    }
}
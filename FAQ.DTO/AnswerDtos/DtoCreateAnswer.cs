﻿using System.ComponentModel.DataAnnotations;

namespace FAQ.DTO.AnswerDtos
{
    public class DtoCreateAnswer
    {
        [Required]
        public Guid QuestionId { get; set; }
        [Required]
        public string Answer { get; set; } = string.Empty;
    }
}
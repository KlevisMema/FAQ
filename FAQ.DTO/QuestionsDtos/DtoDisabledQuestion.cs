﻿namespace FAQ.DTO.QuestionsDtos
{
    public class DtoDisabledQuestion
    {
        public Guid Id { get; set; }
        public string P_Question { get; set; } = string.Empty;
        public DateTime DeletedAt { get; set; }
    }
}
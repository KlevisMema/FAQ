﻿namespace FAQ.DTO.UserDtos
{
    public class DtoUser
    {
        public string Id { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = Enumerable.Empty<string>().ToList();
    }
}
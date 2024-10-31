﻿namespace Survey.Core.Dtos.Authentication.Requests
{
    public class LoginRequest
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}

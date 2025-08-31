﻿using System.ComponentModel.DataAnnotations;

namespace ECommerce517.ViewModels
{
    public class ResendEmailConfirmationVM
    {
        public int Id { get; set; }
        [Required]
        public string EmailOrUserName { get; set; } = string.Empty;
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Conspect_sharing.Models.ManageViewModels
{
    public class IndexViewModel
    {
        public Guid UserId { get; set; }

        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public bool Role { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }
    }
}

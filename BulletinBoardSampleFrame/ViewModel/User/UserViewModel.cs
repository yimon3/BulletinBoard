﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BulletinBoardSampleFrame.ViewModel.User
{
    public class UserViewModel
    {
        [Required]
        [Display(Name="Name")]
        public string name { get; set; }

        [Display(Name = "Email address")]
        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Password must be more than 8 characters long, must contain at least 1 Uppercase and 1 Numeric.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string confirmPassword { get; set; }

        [Required]
        [Display(Name = "Type")]
        public string type { get; set; }

        [Display(Name = "Phone")]
        public string phone { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime dob { get; set; }

        [Display(Name = "Address")]
        public string address { get; set; }

        [Required]
        [Display(Name = "Profile")]
        public string profile { get; set; }

    }
}
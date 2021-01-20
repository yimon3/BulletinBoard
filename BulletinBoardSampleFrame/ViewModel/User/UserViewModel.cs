using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulletinBoardSampleFrame.ViewModel.User
{
    /// <summary>
    /// User getter setter class
    /// </summary>
    public class UserViewModel
    {
        /// <summary>
        /// user id
        /// </summary>
        /// [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        /// <summary>
        /// user name
        /// </summary>
        [Required]
        [Display(Name = "Name")]
        public string name { get; set; }

        /// <summary>
        /// user email
        /// </summary>
        [Display(Name = "Email address")]
        [Required]
        [EmailAddress]
        public string email { get; set; }

        /// <summary>
        /// user password
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "Password must be more than 8 characters long, must contain at least 1 Uppercase and 1 Numeric.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }

        /// <summary>
        /// confirm password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string confirmPassword { get; set; }

        /// <summary>
        /// user type
        /// </summary>
        [Required]
        [Display(Name = "Type")]
        public string type { get; set; }

        /// <summary>
        /// phone number
        /// </summary>
        [Display(Name = "Phone")]
        public string phone { get; set; }

        /// <summary>
        /// date of birth
        /// </summary>
        [Display(Name = "Date of Birth")]
        public DateTime? dob { get; set; }

        /// <summary>
        /// address
        /// </summary>
        [Display(Name = "Address")]
        public string address { get; set; }

        /// <summary>
        /// profile
        /// </summary>
        [Required]
        [Display(Name = "Profile")]
        public string profile { get; set; }

        /// <summary>
        /// createdUser
        /// </summary>
        public string createdUser { get; set; }

        /// <summary>
        /// create date
        /// </summary>
        public DateTime created_at { get; set; } = DateTime.Now;

        /// <summary>
        /// updated date
        /// </summary>
        public DateTime updated_at { get; set; } = DateTime.Now;

    }
}
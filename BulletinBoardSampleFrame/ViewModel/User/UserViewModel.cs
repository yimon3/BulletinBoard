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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// user name
        /// </summary>
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// user email
        /// </summary>
        [Display(Name = "Email address")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// user password
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "Password must be more than 8 characters long, must contain at least 1 Uppercase and 1 Numeric.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        /// confirm password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// user type
        /// </summary>
        [Required]
        [Display(Name = "Type")]
        public string Type { get; set; }

        /// <summary>
        /// phone number
        /// </summary>
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        /// <summary>
        /// date of birth
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}",
               ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Birth")]
        public DateTime? Dob { get; set; }

        /// <summary>
        /// address
        /// </summary>
        [Display(Name = "Address")]
        public string Address { get; set; }

        /// <summary>
        /// profile
        /// </summary>
        [Required]
        [Display(Name = "Profile")]
        public string Profile { get; set; }

        /// <summary>
        /// createdUser
        /// </summary>
        public string CreatedUser { get; set; }

        /// <summary>
        /// create date
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}",
               ApplyFormatInEditMode = true)]
        public DateTime Created_at { get; set; } = DateTime.Now;

        /// <summary>
        /// updated date
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}",
               ApplyFormatInEditMode = true)]
        public DateTime Updated_at { get; set; } = DateTime.Now;

    }
}

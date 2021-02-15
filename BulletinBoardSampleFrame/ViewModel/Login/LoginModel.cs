using System;
using System.ComponentModel.DataAnnotations;

namespace BulletinBoardSampleFrame.ViewModel.Login
{
    /// <summary>
    /// Login getter and setter class
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// user email
        /// </summary>
        [Display(Name = "Email")]
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// user password
        /// </summary>
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// new password
        /// </summary>
        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        /// <summary>
        /// confirm password
        /// </summary>
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("newPassword", ErrorMessage = "New Password and Confirm Password must match.")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// remember me
        /// </summary>
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }

    /// <summary>
    /// Twitter login View Model
    /// </summary>
    public class TwitterViewModel
    {
        /// <summary>
        /// Twitter user's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Tweeet created date
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}",
               ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Tweet
        /// </summary>
        public string Status { get; set; }
    }
}

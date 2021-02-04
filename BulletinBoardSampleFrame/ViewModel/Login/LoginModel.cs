using System.ComponentModel.DataAnnotations;

namespace BulletinBoardSampleFrame.ViewModel.Login
{
    /// <summary>
    /// Login getter and setter class
    /// </summary>
    public class LoginModel
    {
        //user email
        [Display(Name = "Email")]
        [Required]
        public string Email { get; set; }

        //user password
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }


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
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BulletinBoardSampleFrame.ViewModel.Login
{
    public class LoginModel
    {
        //user email
        [Display(Name ="Email")]
        [Required]
        public string email { get; set; }

        //user password
        [Required]
        [Display(Name ="Password")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Display(Name ="Remember Me")]
        public bool rememberMe { get; set; }
    }
}
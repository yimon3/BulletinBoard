using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BulletinBoardSampleFrame.ViewModel.Post
{
     /// <summary>
    ///     Post Getter and Setter class.
    /// </summary>
    public class PostViewModel
    {
        //Post id
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        //Post Title
        [MaxLength(50)]
        [Required]
        [Display(Name ="Title")]
        public string title { get; set; }

        //Post Description
        [Required]
        [Display(Name ="Description")]
        public string description { get; set; }

        //Post Status (Public or Private)
        [Display(Name ="Status")]
        public int status { get; set; }

        //Created Usernmae
        public string name { get; set; }

        //Created Time
        public System.DateTime created_at { get; set; } = DateTime.Now;
    }
}
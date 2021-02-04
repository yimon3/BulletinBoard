using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulletinBoardSampleFrame.ViewModel.Post
{
    /// <summary>
    /// Post Getter and Setter class.
    /// </summary>
    public class PostViewModel
    {
        //Post id
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //Post Title
        [MaxLength(50)]
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        //Post Description
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        //Post Status (Public or Private)
        [Display(Name = "Status")]
        public bool Status { get; set; }

        //Created Usernmae
        public string CreatedUser { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}",
               ApplyFormatInEditMode = true)]
        //Created Time
        public System.DateTime Created_at { get; set; }

        //update user id
        public int UpdatedUserId { get; set; }

        ///<summary>
        /// Gets or sets CurrentPageIndex.
        ///</summary>
        public int CurrentPageIndex { get; set; }

        ///<summary>
        /// Gets or sets PageCount.
        ///</summary>
        public int PageCount { get; set; }

        public int PageNumber { get; set; }
    }
}

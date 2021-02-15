using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulletinBoardSampleFrame.ViewModel.Post
{
    /// <summary>
    /// Post Getter and Setter class.
    /// </summary>
    public class PostViewModel
    {
        /// <summary>
        /// Post id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Post title
        /// </summary>
        [MaxLength(50)]
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        /// <summary>
        /// Post description
        /// </summary>
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// Post status
        /// </summary>
        [Display(Name = "Status")]
        public bool Status { get; set; }

        /// <summary>
        /// Created Usernmae
        /// </summary>
        public string CreatedUser { get; set; }

        /// <summary>
        /// Create date
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}",
               ApplyFormatInEditMode = true)]
        public System.DateTime Created_at { get; set; }

        /// <summary>
        /// update user id
        /// </summary>
        public int UpdatedUserId { get; set; }
    }
}

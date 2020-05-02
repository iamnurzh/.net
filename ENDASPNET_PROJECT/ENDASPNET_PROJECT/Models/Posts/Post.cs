using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ENDASPNET_PROJECT.CustomValidation;
using ENDASPNET_PROJECT.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace ENDASPNET_PROJECT.Models.Posts
{
    public class Post
    {

        [Display(Name = "Post id")]
        public int postId { get; set; }

        [Required(ErrorMessage = "Please enter field")]
        [StringLength(50)]
        [Display(Name = "Post title")]
        public string postTitle { get; set; }

        [Required(ErrorMessage = "Please enter field")]
        [StringLength(2000)]
        [Display(Name = "Post content")]
        public string postContent { get; set; }

        [Required(ErrorMessage = "Please enter field")]
        [StringLength(50)]
        [Display(Name = "Post Author")]
        public string postAuthor{get;set;}

        [Required(ErrorMessage = "Please enter field")]
        [StringLength(20)]
        [Display(Name = "Post category")]
        public string postCategory { get; set; }//many category many posts

        [Required(ErrorMessage = "Please enter  date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [CustomDate(ErrorMessage = "Please be careful!")]
        [Display(Name = "Post time")]
        public DateTime postTime { get; set; }
    }
}

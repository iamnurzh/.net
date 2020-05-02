using ENDASPNET_PROJECT.Models.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ENDASPNET_PROJECT.Models.Comments
{
    public class Comment : IValidatableObject
    {
        [Display(Name = "Comment id")]
        public int commentId { get; set; }

        [Required(ErrorMessage = "Please enter field")]
        [StringLength(500)]
        [Display(Name = "Comment content")]
        public string commentContent { get; set; }

        [Required(ErrorMessage = "Please enter field")]
        [Display(Name = "Comment author ID")]
        public int commentAuthor { get; set; }//one comment one author

        [Required]
        [Display(Name = "Comment time")]
        public DateTime commentTime { get; set; }

        [Display(Name = "Id of post for comment")]
        public int compostID { get; set; }//one post many comment

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var property = new[] { "commentTime" };
            if (commentTime > DateTime.Now.AddMinutes(+1))
            {
                yield return new ValidationResult("Please, check current time.",property);
            }
            if (commentTime < DateTime.Now.AddMinutes(-1))
            {
                yield return new ValidationResult("Please, check current time.",property);
            }
        }
    }
}
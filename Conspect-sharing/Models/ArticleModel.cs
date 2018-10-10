using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Conspect_sharing.Models
{
    public class ArticleModel
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifeDate { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
        public string Specialty { get; set; }
        public Guid UserId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
        public string Name { get; set; }
        public List<CommentModel> Comments { get; set; }
        public List<ArticleTagModel> Tags { get; set; }
        public List<MarkModel> Marks { get; set; }
    }
}

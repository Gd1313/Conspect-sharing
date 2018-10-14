using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conspect_sharing.Models.ViewModels
{
    public class ArticleReadViewModel
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifeDate { get; set; }
        public string Specialty { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public List<CommentViewModel> Comments { get; set; }
        public double Rate { get; set; }
        public string UserName { get; set; }
        public List<string> Tags { get; set; }
        public bool IsAvailableRate { get; set; }
        public int YourMark { get; set; }
    }
}

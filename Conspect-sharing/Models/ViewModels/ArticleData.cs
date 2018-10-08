using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conspect_sharing.Models.ViewModels
{
    public class ArticleData
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifeDate { get; set; }
        public string Specialty { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public List<CommentModel> Comments { get; set; }
        public List<string> Tags { get; set; }
        public List<MarkModel> Marks { get; set; }
    }
}

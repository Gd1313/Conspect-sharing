using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conspect_sharing.Models
{
    public class LikeModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }

        public string CommentId { get; set; }
        public CommentModel Comment { get; set; }
    }
}

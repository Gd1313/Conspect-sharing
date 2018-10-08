using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conspect_sharing.Models
{
    public class LikeModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public Guid CommentId { get; set; }
        public CommentModel Comment { get; set; }
    }
}

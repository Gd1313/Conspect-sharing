using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conspect_sharing.Models
{
    public class CommentModel
    {
    public string Id { get; set; }
    public DateTime Date { get; set; }
    public string Comment { get; set; }
    public string UserId { get; set; }
    public List<LikeModel> Likes { get; set; }



    public string ArticleId { get; set; }
    public ArticleModel Article { get; set; }
}
}

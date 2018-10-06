using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conspect_sharing.Models
{
    public class TagModel
    {
    public string Id { get; set; }
    public string Title { get; set; }
    public List<ArticleTagModel> ArticleTags { get; set; }
}
}

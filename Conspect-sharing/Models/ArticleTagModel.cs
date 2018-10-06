using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conspect_sharing.Models
{
    public class ArticleTagModel
{
    public string Id { get; set; }
    public string ArticleId { get; set; }
    public ArticleModel Article { get; set; }

    public string TagId { get; set; }
    public TagModel Tag { get; set; }
}
}

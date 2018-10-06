using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conspect_sharing.Models
{
    public class MarkModel
    {
    public string Id { get; set; }
    public string UserId { get; set; }
    public int Value { get; set; }

    public string ArticleId { get; set; }
    public ArticleModel Article { get; set; }
}
}

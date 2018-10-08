using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conspect_sharing.Models
{
    public class MarkModel
    {
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public int Value { get; set; }

    public Guid ArticleId { get; set; }
    public ArticleModel Article { get; set; }
}
}

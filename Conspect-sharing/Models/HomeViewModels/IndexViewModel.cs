using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Conspect_sharing.Models;

namespace Conspect_sharing.Models.HomeViewModels
{
    public class IndexViewModel
    {
        public Guid UserId { get; set; }
        public IEnumerable<ArticleModel> Articles { get; set; }
        public PageViewModel PageViewModel { get; set; }
}
}

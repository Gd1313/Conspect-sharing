using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Conspect_sharing.Models;

namespace Conspect_sharing.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<ArticleModel> Articles { get; set; }
        public DbSet<ArticleTagModel> ArticleTags { get; set; }
        public DbSet<CommentModel> Comments { get; set; }
        public DbSet<LikeModel> Likes { get; set; }
        public DbSet<MarkModel> Marks { get; set; }
        public DbSet<TagModel> Tags { get; set; }
        public DbSet<QueryModel> Queries { get; set; }
    }
}

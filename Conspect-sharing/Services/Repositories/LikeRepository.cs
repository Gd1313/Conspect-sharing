using Conspect_sharing.Data;
using Conspect_sharing.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conspect_sharing.Services.Repositories
{
    public class LikeRepository
    {
        ApplicationDbContext Context { get; set; }
        public LikeRepository(ApplicationDbContext context)
        {
            Context = context;
        }


        public IQueryable<LikeModel> GetByCommentId(Guid id)
        {
            return Context.Likes.Where(m => m.CommentId == id);
        }



        public LikeModel Get(Guid id)
        {
            return Context.Likes.Find(id);
        }

        public void Create(LikeModel t)
        {
            Context.Likes.Add(t);
            Context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            LikeModel like = Context.Likes.Find(id);
            Context.Likes.Remove(like);
            Context.SaveChanges();
        }

        public void Update(LikeModel t)
        {
            Context.Likes.Update(t);
            Context.SaveChanges();
        }
    }
}

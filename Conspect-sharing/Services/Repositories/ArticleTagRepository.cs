﻿using Conspect_sharing.Data;
using Conspect_sharing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conspect_sharing.Services.Repositories
{
    public class ArticleTagRepository
    {
        ApplicationDbContext Context { get; set; }
        public ArticleTagRepository(ApplicationDbContext context)
        {
            Context = context;
        }



        public ArticleTagModel Get(Guid id)
        {
            return Context.ArticleTags.Find(id);
        }

        public void Create(ArticleTagModel t)
        {
            Context.ArticleTags.Add(t);
            Context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            ArticleTagModel articleTag = Context.ArticleTags.Find(id);
            Context.ArticleTags.Remove(articleTag);
            Context.SaveChanges();
        }

        public IQueryable<ArticleTagModel> GetByArticleId(Guid id)
        {
            return Context.ArticleTags.Where(t => t.ArticleId == id);
        }

        public void Update(ArticleTagModel t)
        {
            Context.ArticleTags.Update(t);
            Context.SaveChanges();
        }
    }
}

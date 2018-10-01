﻿using Conspect_sharing.Data;
using Conspect_sharing.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conspect_sharing.Services.Repositories
{
    public class ArticleRepository
    {
        ApplicationDbContext applicationDbContext { get; set; }

        public ArticleRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public ArticleModel Get(Guid id)
        {
            return applicationDbContext.Articles.Include(a => a.Comments)
            .ThenInclude(c => c.Likes)
            .Include(a => a.Tags)
            .ThenInclude(t => t.Tag)
            .Include(a => a.Marks)
            .FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<ArticleModel> GetAll()
        {
            return applicationDbContext.Articles
            .Include(a => a.Comments)
            .ThenInclude(c => c.Likes)
            .Include(a => a.Tags)
            .Include(a => a.Marks)
            .ToList();
        }

        //public List<ArticleModel> GetLastModifited(int count)
        //{
        //    return applicationDbContext.Articles
        //    .OrderByDescending(a => a.ModifitedDate)
        //    .Take(count)
        //    .ToList();
        //}

        //public List<ArticleModel> GetWithMarks(int count)
        //{
        //    List<ArticleModel> articles = applicationDbContext.Articles
        //    .Include(a => a.Marks)
        //    .ToList();
        //    return articles
        //    .OrderByDescending(a => Average(a))
        //    .Take(count)
        //    .ToList();

        //}
        //double Average(ArticleModel a)
        //{
        //    if (a.Marks != null && a.Marks.Count() > 0)
        //    {
        //        return a.Marks.Average(m => m.Value);
        //    }
        //    return 0;
        //}

        public void Create(ArticleModel t)
        {
            applicationDbContext.Articles.Add(t);
            applicationDbContext.SaveChanges();
        }

        public void Delete(Guid id)
        {
            ArticleModel article = Get(id);
            applicationDbContext.Articles.Remove(article);
            applicationDbContext.SaveChanges();
        }

        public void Update(ArticleModel t)
        {
            applicationDbContext.Articles.Update(t);
            applicationDbContext.SaveChanges();
        }
    }
}
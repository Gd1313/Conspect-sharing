﻿using Conspect_sharing.Data;
using Conspect_sharing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Conspect_sharing.Services.Repositories
{
    public class MarkRepository
    {
        ApplicationDbContext Context { get; set; }
        public MarkRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public MarkModel Get(Guid id)
        {
            return Context.Marks.Find(id);
        }

        public IQueryable<MarkModel> GetByArticleId(Guid id)
        {
            return Context.Marks.Where(m => m.ArticleId == id);
        }

        public MarkModel Get(Guid ArticeId, Guid userId)
        {
            return Context.Marks.FirstOrDefault(m => m.UserId == userId && m.ArticleId == ArticeId);
        }

        public void Create(MarkModel t)
        {
            Context.Marks.Add(t);
            Context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            MarkModel mark = Context.Marks.Find(id);
            Context.Marks.Remove(mark);
            Context.SaveChanges();
        }

        public void Update(MarkModel t)
        {
            Context.Marks.Update(t);
            Context.SaveChanges();
        }

        public IEnumerable<MarkModel> Find(Expression<Func<MarkModel, bool>> expression)
        {
            return Context.Marks.Where(expression);
        }

    }
}

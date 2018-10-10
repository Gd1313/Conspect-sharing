﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Conspect_sharing.Models;
using Conspect_sharing.Models.ManageViewModels;
using Conspect_sharing.Services;
using Conspect_sharing.Models.ViewModels;
using Conspect_sharing.Services.Repositories;

namespace Conspect_sharing.Controllers
{
    public class ArticleManageController : Controller     
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TagRepository _tagRepository;
        private readonly ArticleRepository _articleRepository;

        public ArticleManageController(UserManager<ApplicationUser> userManager, TagRepository tagRepository, ArticleRepository articleRepository)
        {
            _userManager = userManager;
            _tagRepository = tagRepository;
            _articleRepository = articleRepository;
        }

        [Authorize]
        public IActionResult Index(string userId)
        {
            return View();
        }
        [Authorize]
        public IActionResult Article(string userId)
        {
            var model = new ArticleData() { UserId = new Guid(userId) };
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<string> CreateArticle(ArticleData article)
        {
            var currentUser = await _userManager.FindByIdAsync(article.UserId.ToString());
            ArticleModel model = new ArticleModel()
            {
                Text = article.Text,
                CreatedDate = DateTime.Now,
                LastModifeDate = DateTime.Now,
                Description = article.Description,
                Specialty = article.Specialty,
                Name = article.Name,
                UserId = new Guid(currentUser.Id),
                Tags = CreateArticleTagList(article.Tags, article.Id)
            };
            _articleRepository.Create(model);
            return "/ArticleManage?userId=" + article.UserId;
        }

        [NonAction]
        private List<ArticleTagModel> CreateArticleTagList(List<string> tags, Guid articleId)
        {
            List<ArticleTagModel> articleTagList = new List<ArticleTagModel>();
            if (tags != null)
            {
                foreach (string item in tags)
                {
                    TagModel tag = new TagModel() { Title = item };
                    _tagRepository.Create(tag);
                    articleTagList.Add(new ArticleTagModel()
                    {
                        ArticleId = articleId,
                        TagId = tag.Id
                    });
                }
            }
            return articleTagList;
        }

        [Authorize]
        public async Task<IActionResult> ArticleTable()
        {
            List<ArticleListViewModel> articleListViews = new List<ArticleListViewModel>();
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            List<ArticleModel> articles;
            articles = _articleRepository.GetUserArticle(new Guid(currentUser.Id)).ToList();
            foreach (ArticleModel article in articles)
            {
                articleListViews.Add(new ArticleListViewModel()
                {
                    Name = article.Name,
                    Description = article.Description,
                    Specialty = article.Specialty,
                    Id = article.Id,
                    CreatedDate=article.CreatedDate
                });
            }
            return View(articleListViews);
        }

        [Authorize]
        public async Task<IActionResult> EditArticle(string id)
        {
            ArticleModel article = _articleRepository.Get(new Guid(id));
            if (article != null)
            {
                ArticleModel model = new ArticleModel()
                {
                    Text = article.Text,
                    Description = article.Description,
                    Specialty = article.Specialty,
                    Name = article.Name,
                    UserId =article.UserId,
                    Id = article.Id,
                    CreatedDate = article.CreatedDate,
                    LastModifeDate = article.LastModifeDate,
                    Tags = article.Tags

                    //Tags.Select(t => t.Tag.Title).ToList()
                };
                return View(model);
            }
            return RedirectPermanent("~/ArticleManage/ArticleTable");
        }

        [Authorize]
        [HttpPost]
        public String SaveUpdatedArticle(ArticleModel updatedArticle)
        {
            ArticleModel article = _articleRepository.Get(updatedArticle.Id);
            article.Text = updatedArticle.Text;
            article.Description = updatedArticle.Description;
            article.Specialty = updatedArticle.Specialty;
            article.Name = updatedArticle.Name;
            article.LastModifeDate = DateTime.Now;
            article.Tags = updatedArticle.Tags;
            _articleRepository.Update(article);
            return "/ArticleManage?userId=" + article.UserId;
        }

        [Authorize]
        public async Task<IActionResult> DeleteArticle(string articleId)
        {
            ArticleModel article = _articleRepository.Get(new Guid(articleId));
            if (article != null)
            {
                _articleRepository.Delete(new Guid(articleId));
            }
            return RedirectPermanent("~/ArticleManage/ArticleTable");
        }

        //public async Task<IActionResult> ArticleRead(Guid id)
        //{
        //    ArticleModel article = _articleRepository.Get(id);
        //    //List<CommentViewModel> commentsViewList = new List<CommentViewModel>();
        //    //foreach (CommentModel item in article.Comments)
        //    //{
        //    //    ApplicationUser user = await _userManager.FindByIdAsync(item.UserId.ToString());

        //    //    commentsViewList.Add(new CommentViewModel()
        //    //    {
        //    //        Id = item.Id,
        //    //        Date = item.Date.ToString(),
        //    //        Comment = item.Comment,
        //    //        ArticleId = item.ArticleId,
        //    //        Likes = item.Likes.Count(),
        //    //        Name = user.Name
        //    //    });
        //    //}
        //    List<CommentViewModel> orderedCommentsViewList =
        //    commentsViewList
        //    .OrderByDescending(c => c.Date)
        //    .ToList();
        //    Markdown markdown = new Markdown();
        //    ApplicationUser articleUser =
        //    await _userManager.FindByIdAsync(article.UserId.ToString());
        //    ArticleModel viewModel = new ArticleModel()
        //    {
        //        Id = article.Id,
        //        Text = markdown.Transform(article.Text),
        //        Description = article.Description,
        //        CreatedDate = article.CreatedDate,
        //        LastModifeDate = article.LastModifeDate,
        //        Specialty = article.Specialty,
        //        UserId = narticle.UserId,
        //        Name = article.Name,
        //        Rate = GetAverageRate(article.Marks),
        //        Comments = orderedCommentsViewList,
        //        UserName = articleUser.Name,
        //        Tags = article.Tags.Select(t => t.Tag.Title).ToList()
        //    };
        //    var currentUser = await GetCurrentUser();
        //    if (currentUser != null)
        //    {
        //        MarkModel userMark = article.Marks.FirstOrDefault(m => m.UserId == new Guid(currentUser.Id));
        //        viewModel.IsAvailableRate = currentUser.Id == article.UserId;
        //    }
        //    else
        //        viewModel.IsAvailableRate = true;
        //    return View(viewModel);
        //}
    }
}
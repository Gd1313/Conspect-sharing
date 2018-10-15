using System;
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
using HeyRed.MarkdownSharp;
using Microsoft.AspNetCore.SignalR;

namespace Conspect_sharing.Controllers
{
    public class ArticleManageController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly TagRepository _tagRepository;
        private readonly ArticleRepository _articleRepository;
        private Search _searchService;
        private readonly MarkRepository _markRepository;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly ComentRepository _comentRepository;
        private readonly LikeRepository _likeRepository;

        public ArticleManageController(UserManager<ApplicationUser> userManager, 
            TagRepository tagRepository, ArticleRepository articleRepository, MarkRepository markRepository, IHubContext<ChatHub> hubContext
            , ComentRepository comentRepository, LikeRepository likeRepository, Search search, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _tagRepository = tagRepository;
            _articleRepository = articleRepository;
            _markRepository = markRepository;
            _hubContext = hubContext;
            _comentRepository = comentRepository;
            _likeRepository = likeRepository;
            _searchService = search;
            _signInManager = signInManager;
        }

        [Authorize]
        public async Task<IActionResult> Index(string userId)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            var username = await _userManager.GetUserNameAsync(user);
            var model = new ArticleData() { UserId = new Guid(userId), UserName = username.ToString() };
            return View(model);
        }
        [Authorize]
        public IActionResult Article(string userId)
        {
  
            var model = new ArticleData() { UserId = new Guid(userId)};
         
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
        public async Task<IActionResult> ArticleTable(string userId)
        {
            List<ArticleListViewModel> articleListViews = new List<ArticleListViewModel>();
            var currentUser = await _userManager.FindByIdAsync(userId);
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
                    CreatedDate = article.CreatedDate
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
                ArticleData model = new ArticleData()
                {
                    Text = article.Text,
                    Description = article.Description,
                    Specialty = article.Specialty,
                    Name = article.Name,
                    UserId = article.UserId,
                    Id = article.Id,
                    CreatedDate = article.CreatedDate,
                    LastModifeDate = article.LastModifeDate,
                    Tags = article.Tags.Select(t => t.Tag.Title).ToList()
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
            return RedirectPermanent("~/ArticleManage/ArticleTable?userId=" + article.UserId);
        }

        public async Task<IActionResult> ArticleRead(Guid id)
        {
            ArticleModel article = _articleRepository.Get(id);
            List<CommentViewModel> commentsViewList = new List<CommentViewModel>();
            foreach (CommentModel item in article.Comments)
            {
                ApplicationUser user = await _userManager.FindByIdAsync(item.UserId.ToString());

                commentsViewList.Add(new CommentViewModel()
                {
                    Id = item.Id,
                    Date = item.Date.ToString(),
                    Comment = item.Comment,
                    ArticleId = item.ArticleId,
                    Likes = item.Likes.Count(),
                    Name = user.UserName
                });
            }
            List<CommentViewModel> orderedCommentsViewList =
            commentsViewList
            .OrderByDescending(c => c.Date)
            .ToList();
            Markdown markdown = new Markdown();
            ApplicationUser articleUser =
            await _userManager.FindByIdAsync(article.UserId.ToString());
            var currentUser = await GetCurrentUser();
            int mark=0;
            if (_signInManager.IsSignedIn(User)==false)
            {
                mark = 0;
            }
            else
            {
                if (article.Marks.FirstOrDefault(x => x.UserId == new Guid(currentUser.Id)) == null)
                {
                    mark = 0;
                }
                else
                {
                    mark = article.Marks.First(m => m.UserId == new Guid(currentUser.Id)).Value;
                }
            }
           
            ArticleReadViewModel viewModel = new ArticleReadViewModel()
            {
                Id = article.Id,
                Text = markdown.Transform(article.Text),
                Description = article.Description,
                CreatedDate = article.CreatedDate,
                LastModifeDate = article.LastModifeDate,
                Specialty = article.Specialty,
                UserId = article.UserId,
                Name = article.Name,
                Rate = GetAverageRate(article.Marks),
                Comments = orderedCommentsViewList,
                UserName = articleUser.UserName,
                Tags = article.Tags.Select(t => t.Tag.Title).ToList(),
                YourMark= mark
            };

            if (currentUser != null)
            {
                MarkModel userMark = article.Marks.FirstOrDefault(m => m.UserId == new Guid(currentUser.Id));
                viewModel.IsAvailableRate = currentUser.Id == article.UserId.ToString();
            }
            else
                viewModel.IsAvailableRate = true;
            return View(viewModel);
        }

        [NonAction]
        private double GetAverageRate(List<MarkModel> marks)
        {
            double rate = 0;
            if (marks != null && marks.Count() > 0)
            {
                rate = marks.Select(p => p.Value).Average();
            }
            return rate;
        }

        [NonAction]
        private async Task<ApplicationUser> GetCurrentUser()
        {
            if (User.Identity.Name != null)
            {
                return await _userManager.FindByNameAsync(User.Identity.Name);
            }
            return null;
        }

    
        public IActionResult SearchByHashtag(string hashtag)
        {
            IEnumerable<ArticleModel> searchResult = _searchService.GetByHashtag(hashtag);
            List<ArticleListViewModel> articleLists = new List<ArticleListViewModel>();
            if (searchResult != null)
            {
                articleLists = CreateArticleList(searchResult.ToList());
            }
            return View("SearchResults", articleLists);
        }

        [NonAction]
        public List<ArticleListViewModel> CreateArticleList(List<ArticleModel> articleModels)
        {
            List<ArticleListViewModel> articlesLists = new List<ArticleListViewModel>();
            if (articleModels != null)
            {
                foreach (ArticleModel article in articleModels)
                {
                    articlesLists.Add(new ArticleListViewModel()
                    {
                        Name = article.Name,
                        Description = article.Description,
                        Specialty = article.Specialty,
                        Id = article.Id,
                        LastModifeDate = article.LastModifeDate,
                        Rate = GetAverageRate(_markRepository.GetByArticleId(article.Id).ToList())
                    });
                }
            }
            return articlesLists;
        }

        [Authorize]
        [HttpPost]
        public async Task SetComment(string articleId, string text)
        {
            var userId = (await GetCurrentUser()).Id;
            CommentModel comment = new CommentModel()
            {
                Comment = text,
                Date = DateTime.Now,
                ArticleId = new Guid(articleId),
                UserId = new Guid(userId)
            };
            ArticleModel article = _articleRepository.Get(new Guid(articleId));
            article.Comments.Add(comment);
            _articleRepository.Update(article);
            await SendComment(comment);
        }

        [NonAction]
        private async Task SendComment(CommentModel comment)
        {
            ApplicationUser user = await _userManager
                .FindByIdAsync(comment.UserId.ToString());
            CommentViewModel commentForSend = new CommentViewModel()
            {
                Comment = comment.Comment,
                Date = comment.Date.ToString(),
                ArticleId = comment.ArticleId,
                Name = user.UserName,
                Likes = 0,
                Id = comment.Id
            };
            await _hubContext.Clients
                .All
                .SendAsync("SendComment", commentForSend);
        }

        [Authorize]
        public async Task ChangeUserName(string pk, string value)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(pk);
            if (user != null)
            {
                user.UserName = value;
                await _userManager.UpdateAsync(user);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<object> SetLikeToComment(string id)
        {
            var userId = (await GetCurrentUser()).Id;
            CommentModel comment = _comentRepository.Get(new Guid(id));
            LikeModel likeToThisComment = comment.Likes
                .FirstOrDefault(l => l.UserId == new Guid(userId));
            if (likeToThisComment == null )
            {
                LikeModel like = new LikeModel()
                {
                    CommentId = new Guid(id),
                    UserId = new Guid(userId)
                };
                comment.Likes.Add(like);
                _likeRepository.Update(like);
                return new { Success = true, Id = id, Likes = comment.Likes.Count() };
            }
            return new { Success = false, Id = id };
        }

        [Authorize]
        [HttpPost]
        public async Task SetRate(string articleId, int rate)
        {
            int doubleRate = rate;
            var userId = (await GetCurrentUser()).Id;
            ArticleModel article = _articleRepository.Get(new Guid(articleId));
            MarkModel userMark = article.Marks
                .FirstOrDefault(m => m.UserId == new Guid(userId));
          
                if (userMark != null)
                {
                    userMark.Value = rate;
                    _markRepository.Update(userMark);
                }
                else
                {
                    MarkModel newMark = new MarkModel()
                    {
                        UserId = new Guid(userId),
                        ArticleId = new Guid(articleId),
                        Value = doubleRate
                    };
                    article.Marks.Add(newMark);
                    _articleRepository.Update(article);
                }
            
        }
    }
}
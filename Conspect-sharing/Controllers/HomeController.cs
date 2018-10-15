using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Conspect_sharing.Models;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using Conspect_sharing.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Conspect_sharing.Services.Repositories;
using Conspect_sharing.Models.HomeViewModels;

namespace Conspect_sharing.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ArticleRepository _articleRepository;
        private readonly TagRepository _tagRepository;
        private readonly MarkRepository _markRepository;

        public HomeController(UserManager<ApplicationUser> userManager, ArticleRepository articleRepository, TagRepository tagRepository,
            MarkRepository markRepository)
        {
            _userManager = userManager;
            _articleRepository = articleRepository;
            _tagRepository = tagRepository;
            _markRepository = markRepository;
        }

        public async Task<IActionResult> TopArticles()
        {
            List<ArticleModel> topArticles = _articleRepository.GetWithTopRating();
            TopArticlesViewModel model = new TopArticlesViewModel();
            model.TopRating= CreateArticleList(
                topArticles);
            return View(model);
        }

        public async Task<IActionResult> LastArticles()
        {
            List<ArticleModel> lastmodifed = _articleRepository.GetLastModifited(5);
            LatestViewModel model = new LatestViewModel();
            model.LatestModified = CreateArticleList(
                lastmodifed);
            return View(model);
        }

        public async Task<IActionResult> Index(int page=1)
        {
            int pageSize = 10;
            IQueryable<ArticleModel> source = _articleRepository.applicationDbContext.Articles;
            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel();
            List<TagViewModel> tags = new List<TagViewModel>();

            var user = await _userManager.GetUserAsync(User);

            if (user!=null)
            {
                viewModel = new IndexViewModel
                {
                    UserId = new Guid(user.Id),
                    PageViewModel = pageViewModel,
                    Articles = items
                };
           
                foreach (TagModel tag in _tagRepository.GetAllTags())
                {
                    tags.Add(new TagViewModel() { Id = tag.Id, Title = tag.Title, Width = tag.ArticleTags.Count() });
                }
                viewModel.Tags = tags;

                return View(viewModel);
            }

         

            source = _articleRepository.applicationDbContext.Articles;
            count = source.Count();
            items = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            pageViewModel = new PageViewModel(count, page, pageSize);

            viewModel = new IndexViewModel
            {   
                PageViewModel = pageViewModel,
                Articles = items
            };
            tags = new List<TagViewModel>();
            foreach (TagModel tag in _tagRepository.GetAllTags())
            {
                tags.Add(new TagViewModel() { Id = tag.Id, Title = tag.Title, Width = tag.ArticleTags.Count() });
            }
            viewModel.Tags = tags;

            return View(viewModel);

        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [NonAction]
        public string GetCulture()
        {
            return $"CurrentCulture:{CultureInfo.CurrentCulture.Name}, CurrentUICulture:{CultureInfo.CurrentUICulture.Name}";
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
                        Rate = GetAverageRate(_markRepository.Find(x => x.ArticleId == article.Id))
                    });
                }
            }
            return articlesLists;
        }

        [NonAction]
        private double GetAverageRate(IEnumerable<MarkModel> marks)
        {
            double rate = 0;
            if (marks != null && marks.Count() > 0)
            {
                rate = marks.Select(p => p.Value).Average();
            }
            return rate;
        }
    }
}

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

        public HomeController(UserManager<ApplicationUser> userManager, ArticleRepository articleRepository)
        {
            _userManager = userManager;
            _articleRepository = articleRepository;
        }

        public async Task<IActionResult> Index(int page=1)
        {
            int pageSize = 10;
            IQueryable<ArticleModel> source = _articleRepository.applicationDbContext.Articles;
            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel();
            var user = await _userManager.GetUserAsync(User);
            if (user!=null)
            {
                viewModel = new IndexViewModel
                {
                    UserId = new Guid(user.Id),
                    PageViewModel = pageViewModel,
                    Articles = items
                };

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
    }
}

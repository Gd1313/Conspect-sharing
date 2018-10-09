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

namespace Conspect_sharing.Controllers
{
    public class ArticleManageController : Controller     
    {
        private Guid ArcticleUserId;

        public IActionResult Index(string userId)
        {
            return View();
        }
        public IActionResult Article(string userId)
        {
            var model = new ArticleData() { UserId = new Guid(userId) };
            return View(model);
        }

    }
}
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinaryClinic.Services;

namespace VeterinaryClinic.Web.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsScraperService newsScraperService;

        public NewsController(INewsScraperService newsScraperService)
        {
            this.newsScraperService = newsScraperService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Add()
        {
            await this.newsScraperService.PopulateDbWithNews();
            return this.RedirectToAction("Index", "Home");
        }
    }
}

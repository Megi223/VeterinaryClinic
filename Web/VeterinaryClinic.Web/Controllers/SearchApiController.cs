﻿namespace VeterinaryClinic.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using VeterinaryClinic.Services.Data;

    [Route("api/[controller]")]
    [ApiController]
    public class SearchApiController : ControllerBase
    {
        private readonly ISearchService searchService;

        public SearchApiController(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        [Produces("application/json")]
        [HttpGet]
        public async Task<IActionResult> Get(bool veterinarians, bool services, bool news)
        {
            try
            {
                string term = this.HttpContext.Request.Query["term"].ToString();

                var vetsFound = new List<string>();
                var servicesFound = new List<string>();
                var newsFound = new List<string>();
                if (veterinarians == true)
                {
                    vetsFound = this.searchService.SearchVet(term);
                }

                if (services == true)
                {
                    servicesFound = this.searchService.SearchServices(term);
                }

                if (news == true)
                {
                    newsFound = this.searchService.SearchNews(term);
                }

                List<string> found = new List<string>();
                found.AddRange(vetsFound);
                found.AddRange(servicesFound);
                found.AddRange(newsFound);

                return this.Ok(found);
            }
            catch
            {
                return this.BadRequest();
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinaryClinic.Data.Common.Repositories;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Web.ViewModels.Reviews;
using VeterinaryClinic.Services.Mapping;


namespace VeterinaryClinic.Web.ViewComponents
{
    public class ReviewsViewComponent : ViewComponent
    {
        private readonly IDeletableEntityRepository<Review> reviewsRepository;

        public ReviewsViewComponent(IDeletableEntityRepository<Review> reviewsRepository)
        {
            this.reviewsRepository = reviewsRepository;
        }

        public IViewComponentResult Invoke()
        {
            var model = new ReviewViewModel
            {
                Reviews = this.reviewsRepository.AllAsNoTracking().OrderByDescending(x => x.CreatedOn).To<SingleReviewViewModel>().Take(7).ToList(),
            };

            return this.View(model);
        }
    }
}

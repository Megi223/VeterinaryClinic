namespace VeterinaryClinic.Web.ViewComponents
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using VeterinaryClinic.Data.Common.Repositories;
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services.Mapping;
    using VeterinaryClinic.Web.ViewModels.Reviews;

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

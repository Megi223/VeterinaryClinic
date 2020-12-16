namespace VeterinaryClinic.Web.ViewComponents
{

    using Microsoft.AspNetCore.Mvc;
    using VeterinaryClinic.Services.Data;
    using VeterinaryClinic.Web.ViewModels.Reviews;

    public class ReviewsViewComponent : ViewComponent
    {
        private readonly IReviewsService reviewsService;

        public ReviewsViewComponent(IReviewsService reviewsService)
        {
            this.reviewsService = reviewsService;
        }

        public IViewComponentResult Invoke()
        {
            var model = new ReviewViewModel
            {
                Reviews = this.reviewsService.GetLatestReviews<SingleReviewViewModel>(),
            };

            return this.View(model);
        }
    }
}

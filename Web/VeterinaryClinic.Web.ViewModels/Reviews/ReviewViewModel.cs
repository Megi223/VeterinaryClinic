namespace VeterinaryClinic.Web.ViewModels.Reviews
{
    using System.Collections.Generic;

    public class ReviewViewModel
    {
        public IEnumerable<SingleReviewViewModel> Reviews { get; set; }
    }
}
